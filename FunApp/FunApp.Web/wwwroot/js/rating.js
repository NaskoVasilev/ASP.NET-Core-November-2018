$("#rate-btn").click(rateEventHendler);

function rateEventHendler(e) {
    let rateButton = $(e.target);
    let jokeId = rateButton.attr("data-id");
    let rating = $("#rating").val();

    if (Number.parseInt(rating) <= 0 || Number.parseInt(rating) > 6) {
        $("span.text-danger").text("The rating must be between 1 and 6!");
        return;
    }

    $.post('/Joke/Rate', { rating, jokeId })
        .done((newRating) => {
            $("span.text-danger").text("");
            $("#joke-rating").text(newRating);
        })
        .catch(err => {
            console.log(err.responseText);
            console.log(err.responseJSON);
            $("span.text-danger").text(err.responseJSON.message);
            $("#rating").val("");
        });

    $("#rating").val("");
}
