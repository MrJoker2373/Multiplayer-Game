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

	$roomRaw = mysqli_real_escape_string($connection, $_POST["room"]);
	$room = filter_var($roomRaw, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
	$door = $_POST["door"];

	$query = mysqli_query($connection, "SELECT roomIndex, doorLeft, doorRight, doorTop, doorBottom FROM rooms WHERE roomIndex='" . $room . "';") or die();

	if(mysqli_num_rows($query) != 1)
	{
		echo("Room is not exists");
		exit();
	}

	$data = mysqli_fetch_assoc($query);
	$index = $data["door" . $door];

	echo($index);
?>