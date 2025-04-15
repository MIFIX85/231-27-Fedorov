<?php
session_start();

// Включаем вывод всех ошибок для отладки
error_reporting(E_ALL);
ini_set('display_errors', 1);

// Конфигурация базы данных
$db_host = 'localhost';
$db_user = 'root';
$db_pass = '';
$db_name = 'forma';

$response = ['success' => false, 'message' => ''];

// Если форма отправлена
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $email = trim($_POST['email'] ?? '');
    $password = $_POST['password'] ?? '';

    error_log("Attempting login for email: " . $email);

    // Проверяем наличие данных
    if (empty($email) || empty($password)) {
        $response['message'] = 'Все поля обязательны для заполнения';
        error_log("Empty email or password");
    } else {
        // Улучшенная валидация email
        if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
            $response['message'] = 'Неверный формат email';
            error_log("Invalid email format: " . $email);
        } else {
            // Проверка длины email
            if (strlen($email) > 255) {
                $response['message'] = 'Email слишком длинный';
                error_log("Email too long: " . $email);
            } else {
                // Проверка длины пароля
                if (strlen($password) < 8) {
                    $response['message'] = 'Пароль должен содержать минимум 8 символов';
                    error_log("Password too short");
                } else {
                    try {
                        error_log("Attempting database connection...");
                        // Создаем подключение с использованием PDO
                        $dsn = "mysql:host=$db_host;dbname=$db_name;charset=utf8mb4";
                        $options = [
                            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
                            PDO::ATTR_EMULATE_PREPARES => false,
                        ];
                        
                        $pdo = new PDO($dsn, $db_user, $db_pass, $options);
                        error_log("Database connection successful");
                        
                        // Подготовленный запрос для проверки существования пользователя
                        $stmt = $pdo->prepare("SELECT id, name, email, password FROM users WHERE email = ?");
                        $stmt->execute([$email]);
                        $user = $stmt->fetch();

                        if (!$user) {
                            error_log("User not found: " . $email);
                            $response['message'] = 'Неверный email или пароль';
                        } else {
                            error_log("User found, verifying password...");
                            // Проверка пароля
                            if (!password_verify($password, $user['password'])) {
                                error_log("Invalid password for user: " . $email);
                                $response['message'] = 'Неверный email или пароль';
                            } else {
                                error_log("Login successful for user: " . $email);
                                
                                // Проверяем наличие необходимых данных
                                if (empty($user['name']) || empty($user['email'])) {
                                    error_log("Missing user data: name or email is empty");
                                    $response['message'] = 'Ошибка данных пользователя';
                                } else {
                                    // Успешная авторизация
                                    $_SESSION['user'] = [
                                        'id' => $user['id'],
                                        'name' => $user['name'],
                                        'email' => $user['email']
                                    ];
                                    
                                    // Логируем сохраненные данные
                                    error_log("Session data saved: " . print_r($_SESSION['user'], true));
                                    
                                    $response['success'] = true;
                                    $response['message'] = 'Успешный вход';
                                    $response['redirect'] = 'success.html';
                                }
                            }
                        }
                        
                    } catch (PDOException $e) {
                        error_log("Database error: " . $e->getMessage());
                        $response['message'] = 'Произошла ошибка при подключении к базе данных: ' . $e->getMessage();
                    }
                }
            }
        }
    }
}

// Отправляем JSON-ответ
header('Content-Type: application/json');
echo json_encode($response);
exit;
?>