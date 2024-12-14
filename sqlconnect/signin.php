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

	$query = mysqli_query($connection, "SELECT nickname, salt, hash FROM users WHERE nickname='" . $nickname . "';") or die();

	if(mysqli_num_rows($query) != 1)
	{
		echo("User is not exists");
		exit();
	}

	$login = mysqli_fetch_assoc($query);

	$salt = $login["salt"];
	$hash = $login["hash"];

	$cryptedhash = crypt($password, $salt);
	if($cryptedhash != $hash)
	{
		echo("Wrong password");
		exit();
	}

	echo("0");
?>