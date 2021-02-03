<?php
        $file = file("qoutes.txt");
        $num_lines = count($file);
        $last_index = $num_lines - 1;
        echo json_encode(array("data"=>$file));
?>