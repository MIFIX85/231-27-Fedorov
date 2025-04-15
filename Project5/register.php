<?php
header('Content-Type: application/json');

// Включение отображения ошибок для отладки
error_reporting(E_ALL);
ini_set('display_errors', 1);

// Подключение к базе данных
$host = 'localhost';
$dbname = 'forma';
$username = 'root';
$password = '';

try {
    // Сначала пробуем подключиться к MySQL без выбора базы данных
    $pdo = new PDO("mysql:host=$host", $username, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    // Проверяем существование базы данных
    $result = $pdo->query("SHOW DATABASES LIKE '$dbname'");
    if ($result->rowCount() == 0) {
        // Создаем базу данных, если она не существует
        $pdo->exec("CREATE DATABASE IF NOT EXISTS `$dbname` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci");
    }
    
    // Подключаемся к конкретной базе данных
    $db = new PDO("mysql:host=$host;dbname=$dbname;charset=utf8mb4", $username, $password);
    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    // Проверка существования таблицы users
    $tableExists = $db->query("SHOW TABLES LIKE 'users'")->rowCount() > 0;
    
    if (!$tableExists) {
        // Создание таблицы users, если она не существует
        $db->exec("CREATE TABLE IF NOT EXISTS users (
            id INT AUTO_INCREMENT PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            email VARCHAR(255) NOT NULL UNIQUE,
            phone VARCHAR(20) DEFAULT NULL,
            password VARCHAR(255) NOT NULL,
            created_at DATETIME NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci");
    } else {
        // Проверяем существование всех необходимых столбцов
        $requiredColumns = [
            'name' => 'VARCHAR(255) NOT NULL',
            'email' => 'VARCHAR(255) NOT NULL UNIQUE',
            'phone' => 'VARCHAR(20) DEFAULT NULL',
            'password' => 'VARCHAR(255) NOT NULL',
            'created_at' => 'DATETIME NOT NULL'
        ];
        
        $existingColumns = $db->query("SHOW COLUMNS FROM users")->fetchAll(PDO::FETCH_COLUMN);
        
        foreach ($requiredColumns as $column => $definition) {
            if (!in_array($column, $existingColumns)) {
                $db->exec("ALTER TABLE users ADD COLUMN $column $definition");
            }
        }
        
        // Проверяем и добавляем UNIQUE индекс для email, если его нет
        $indexes = $db->query("SHOW INDEX FROM users WHERE Column_name = 'email'")->fetchAll();
        if (empty($indexes)) {
            $db->exec("ALTER TABLE users ADD UNIQUE INDEX idx_email (email)");
        }
    }
} catch (PDOException $e) {
    error_log("Database Error: " . $e->getMessage());
    echo json_encode([
        'success' => false,
        'message' => 'Ошибка подключения к базе данных: ' . $e->getMessage()
    ]);
    exit;
}

// Проверка метода запроса
if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    echo json_encode([
        'success' => false,
        'message' => 'Неверный метод запроса'
    ]);
    exit;
}

// Обработка данных формы
$errors = [];
$name = trim($_POST['name'] ?? '');
$email = trim($_POST['email'] ?? '');
$phone = trim($_POST['phone'] ?? '');
$password = $_POST['password'] ?? '';
$confirm_password = $_POST['confirm_password'] ?? '';

// Валидация
if (empty($name)) {
    $errors['name'] = 'Введите имя и фамилию';
}

if (empty($email)) {
    $errors['email'] = 'Введите email';
} elseif (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
    $errors['email'] = 'Некорректный email';
} else {
    // Проверка на существующий email
    try {
        $stmt = $db->prepare("SELECT id FROM users WHERE email = ?");
        $stmt->execute([$email]);
        if ($stmt->fetch()) {
            $errors['email'] = 'Этот email уже зарегистрирован';
        }
    } catch (PDOException $e) {
        error_log("Email check error: " . $e->getMessage());
        $errors['email'] = 'Ошибка при проверке email: ' . $e->getMessage();
    }
}

// Валидация телефона
if (!empty($phone) && !preg_match('/^\+7\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}$/', $phone)) {
    $errors['phone'] = 'Введите номер телефона в формате +7 (XXX) XXX-XX-XX';
}

if (empty($password)) {
    $errors['password'] = 'Введите пароль';
} elseif (strlen($password) < 8) {
    $errors['password'] = 'Пароль должен содержать не менее 8 символов';
}

if ($password !== $confirm_password) {
    $errors['confirm'] = 'Пароли не совпадают';
}

// Если есть ошибки - возвращаем их
if (!empty($errors)) {
    echo json_encode([
        'success' => false,
        'errors' => $errors
    ]);
    exit;
}

// Хеширование пароля
$hashed_password = password_hash($password, PASSWORD_DEFAULT);

// Сохранение пользователя в БД
try {
    $stmt = $db->prepare("INSERT INTO users (name, email, phone, password, created_at) VALUES (?, ?, ?, ?, NOW())");
    $result = $stmt->execute([$name, $email, $phone, $hashed_password]);
    
    if ($result) {
        echo json_encode([
            'success' => true,
            'message' => 'Регистрация прошла успешно!',
            'redirect' => 'success.html'
        ]);
    } else {
        throw new Exception("Failed to insert user data");
    }
} catch (PDOException $e) {
    error_log("Registration error: " . $e->getMessage());
    echo json_encode([
        'success' => false,
        'message' => 'Ошибка при сохранении данных: ' . $e->getMessage()
    ]);
} catch (Exception $e) {
    error_log("General error: " . $e->getMessage());
    echo json_encode([
        'success' => false,
        'message' => 'Произошла ошибка: ' . $e->getMessage()
    ]);
}