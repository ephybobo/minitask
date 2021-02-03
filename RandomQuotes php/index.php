<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Random quote</title>

    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="style.css">
</head>

<body>
    <div class="wrapper">
        <div class="scrollbar" id="style-6">
            <div class="force-overflow">

                <div onclick="showIt()" class="repeat btn btn-warning">Show it</div>

                <div class="div">
                    <div id="listQuotes"></div>
                </div>
            </div>
        </div>
    </div>
</body>

</html>

<script>
    let allQuotes =[];
    let showQuotes =[];
    $(document).ready(function() {
        generate();
    });

    function generate () {
        fetch('./api.php')
        .then((resp) => resp.json())
        .then(function(data) {
            allQuotes = data.data;
            showIt();
        })
        .catch(function(error) {
            console.log(error);
        });
    }
    
    function showIt() {
        if(allQuotes.length > 0){
            let nQuotes = allQuotes.length;
            let chsQuotes = Math.floor((Math.random() * nQuotes));
            showQuotes.push(allQuotes[chsQuotes]);
            console.log(chsQuotes+" of : "+nQuotes+" : "+showQuotes.length);
            allQuotes.splice(chsQuotes, 1);
            let showText="";
            let date = new Date();
            for(a=0;a<showQuotes.length;a++){
                showText += `<b>Qoutes of the day :</b> <br>`;
                showText += `${showQuotes[a]}`; 
                showText += `<br> <em>Time clicked : ${date}</em> <br/>`;
                showText += ` ============================================ <br> `;
            }
        document.getElementById("listQuotes").innerHTML = showText;
        }
        else 
            generate();

    }
</script>