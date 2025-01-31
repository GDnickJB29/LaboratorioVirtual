<?php
// login.php
session_start();
include 'api/config.php';

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $nombre = $conn->real_escape_string($_POST['nombre']);
    // Verificamos si el usuario ya existe
    $result = $conn->query("SELECT * FROM usuarios WHERE nombre = '$nombre'");
    
    if ($result->num_rows > 0) {
        // Si el usuario ya existe, iniciamos sesión y redirigimos
        $_SESSION['usuario'] = $nombre;
        header('Location: bienvenido.php');
        exit();
    } else {
        // Si no existe el usuario, lo creamos
        $insert = $conn->query("INSERT INTO usuarios (nombre) VALUES ('$nombre')");
        if ($insert) {
            // Si la inserción fue exitosa, iniciamos la sesión y redirigimos
            $_SESSION['usuario'] = $nombre;
            header('Location: bienvenido.php');
            exit();
        } else {
            // Si ocurre un error al crear el usuario
            echo "<p>Error al crear el usuario.</p>";
        }
    }
}
?>
<!DOCTYPE html>
<html>
<head><title>Login</title></head>
<body>
    <form method="post" action="">
        <label>Nombre de usuario:</label>
        <input type="text" name="nombre" required>
        <button type="submit">Ingresar</button>
    </form>
</body>
</html>
