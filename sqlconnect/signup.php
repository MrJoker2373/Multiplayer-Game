<?php

	$myServer = "localhost";
	$myUsername = "new_bd_usr";
	$myPassword = "5q2i7TfTZMJtapWv";
	$myDatabase = "new_bd";

	$connection = mysqli_connect($myServer, $myUsername, $myPassword, $myDatabase);

	if(mysqli_connect_errno())
	{
		echo("Connection error");
		exit();
	}

	$nicknameRaw = mysqli_real_escape_string($connection, $_POST["nickname"]);
	$nickname = filter_var($nicknameRaw, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
	$password = $_POST["password"];
	$email = $_POST["email"];

	$query = mysqli_query($connection, "SELECT nickname FROM users WHERE nickname='" . $nickname . "';") or die();

	if(mysqli_num_rows($query) > 0)
	{
		echo("User already exists");
		exit();
	}

	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $nickname . "\$";
	$hash = crypt($password, $salt);

	mysqli_query($connection, "INSERT INTO users (nickname, hash, salt, email) VALUES ('" . $nickname . "', '" . $hash . "', '" . $salt . "', '" . $email . "');") or die();

	echo("0");
?>