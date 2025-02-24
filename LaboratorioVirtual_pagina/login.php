<?php
// login.php
session_start();
include 'api/config.php';

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $nombre = $conn->real_escape_string($_POST['nombre']);
    $result = $conn->query("SELECT * FROM usuarios WHERE nombre = '$nombre'");

    if ($result->num_rows > 0) {
        $_SESSION['usuario'] = $nombre;
        header('Location: welcome.php');
        exit();
    } else {
        $insert = $conn->query("INSERT INTO usuarios (nombre) VALUES ('$nombre')");
        if ($insert) {
            $_SESSION['usuario'] = $nombre;
            header('Location: welcome.php');
            exit();
        } else {
            $error = "Error al crear el usuario.";
        }
    }
}
?>
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iniciar Sesión</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600&display=swap">
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Poppins', sans-serif;
        }

        body {
            background: #1e1e2e;
            color: #ffffff;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .login-container {
            background: #2b2b3c;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(255, 255, 255, 0.1);
            text-align: center;
            width: 300px;
        }

        h2 {
            margin-bottom: 15px;
            font-weight: 600;
        }

        input {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: none;
            border-radius: 5px;
            font-size: 16px;
        }

        button {
            width: 100%;
            background: #007bff;
            color: white;
            padding: 10px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            transition: 0.3s;
        }

        button:hover {
            background: #0056b3;
        }

        .error {
            color: #ff4444;
            margin-top: 10px;
            font-size: 14px;
        }
    </style>
</head>

<body>
    <div class="login-container">
        <h2>Iniciar Sesión</h2>
        <form method="post" action="">
            <input type="text" name="nombre" placeholder="Nombre de usuario" required>
            <button type="submit">Ingresar</button>
        </form>
        <?php if (isset($error)) : ?>
            <p class="error"><?php echo $error; ?></p>
        <?php endif; ?>
    </div>
</body>

</html>
