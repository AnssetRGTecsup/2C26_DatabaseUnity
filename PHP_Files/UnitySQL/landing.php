<?php

/**
 * This PHP script displays the current date and time.
 * It uses the `date()` function to format the output.
 */

// Set the default timezone to avoid warnings and ensure accurate time.
// You can change 'America/New_York' to your desired timezone,
// for example, 'Europe/London', 'Asia/Tokyo', or 'America/Lima'.
date_default_timezone_set('America/Lima');

// Get the current date and time using the date() function.
// The 'Y-m-d H:i:s' format string means:
// Y: Full year (e.g., 2024)
// m: Month as a two-digit number (e.g., 01 for January, 12 for December)
// d: Day of the month as a two-digit number (e.g., 01 to 31)
// H: Hour in 24-hour format (e.g., 00 to 23)
// i: Minutes (e.g., 00 to 59)
// s: Seconds (e.g., 00 to 59)
$currentDateTime = date('Y-m-d H:i:s');

// Display the current date and time to the user.
echo "The current date and time is: " . $currentDateTime;

echo "<br><br>"; // Add a line break for better readability in HTML output

// You can also display specific parts of the date and time:
echo "Today's date: " . date('l, F j, Y'); // Example: Wednesday, June 5, 2024
echo "<br>";
echo "Current time: " . date('h:i:s A'); // Example: 08:34:00 AM

?>
