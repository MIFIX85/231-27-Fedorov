<?php
// Настройки подключения к MySQL
$host = 'localhost';
$username = 'root';
$password = '';

try {
    // Подключение к MySQL без выбора базы данных
    $pdo = new PDO("mysql:host=$host", $username, $password);
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    // Чтение SQL файла
    $sql = file_get_contents('setup_database.sql');
    
    // Выполнение SQL запросов
    $pdo->exec($sql);
    
    echo "База данных 'forma' и таблица 'users' успешно созданы!\n";
    
} catch (PDOException $e) {
    die("Ошибка при создании базы данных: " . $e->getMessage() . "\n");
}
?> 