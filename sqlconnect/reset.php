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

	$emailRaw = mysqli_real_escape_string($connection, $_POST["email"]);
	$email = filter_var($emailRaw, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

	$query = mysqli_query($connection, "SELECT email FROM users WHERE email='" . $email . "';") or die();

	if(mysqli_num_rows($query) != 1)
	{
		echo("Email is not exists");
		exit();
	}

	echo("0");
?>