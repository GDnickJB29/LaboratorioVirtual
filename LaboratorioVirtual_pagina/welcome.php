<?php
session_start();
if (!isset($_SESSION['usuario'])) {
    header('Location: login.php');
    exit();
}
include 'api/config.php';

$usuario = $_SESSION['usuario'];
?>
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bienvenido</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600&display=swap">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
            text-align: center;
            padding: 20px;
        }

        h1 {
            font-weight: 600;
            margin-bottom: 10px;
        }

        #score {
            font-size: 1.5em;
            font-weight: bold;
            color: #ffcc00;
        }

        #gameContainer {
            margin: 20px auto;
            padding: 10px;
            background: #2b2b3c;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(255, 255, 255, 0.2);
            display: inline-block;
        }

        iframe {
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(255, 255, 255, 0.1);
        }

        .btn-container {
            margin-top: 15px;
        }

        button {
            background: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            margin: 5px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            transition: 0.3s;
        }

        button:hover {
            background: #0056b3;
        }

        .logout-btn {
            background: #ff4444;
        }

        .logout-btn:hover {
            background: #cc0000;
        }
    </style>
    <script>
        function actualizarPuntuacion() {
            $.get("api/actualizar_puntuacion.php")
                .done(function (data) {
                    $("#score").text(data);
                })
                .fail(function () {
                    console.error("Error al actualizar la puntuación.");
                });
        }

        setInterval(actualizarPuntuacion, 1000);

        function setFullScreen() {
            let iframe = document.getElementById("unityFrame");
            if (iframe.requestFullscreen) {
                iframe.requestFullscreen();
            } else if (iframe.mozRequestFullScreen) {
                iframe.mozRequestFullScreen();
            } else if (iframe.webkitRequestFullscreen) {
                iframe.webkitRequestFullscreen();
            } else if (iframe.msRequestFullscreen) {
                iframe.msRequestFullscreen();
            }
        }
    </script>
</head>

<body>
    <h1>Bienvenido <?php echo htmlspecialchars($usuario); ?></h1>
    <p>Puntuación: <span id="score">Cargando...</span></p>

    <div id="gameContainer">
        <iframe id="unityFrame" src="builds_unity/index.html" width="1280" height="720"></iframe>
    </div>

    <div class="btn-container">
        <button onclick="setFullScreen()">Pantalla Completa</button>
    </div>

    <form action="api/logout.php" method="post">
        <button type="submit" class="logout-btn">Cerrar sesión</button>
    </form>
</body>

</html>
