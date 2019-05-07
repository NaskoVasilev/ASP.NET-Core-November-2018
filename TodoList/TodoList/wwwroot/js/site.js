$("#add").click(add);
$(".edit").click(editHandler);
$(".delete").click(deleteHandler);

function add() {
    let endDate = $("#endDateAdd").val();
    let content = $("#contentAdd").val();

    $.post("/Task/Create", { content, endDate })
        .done((task) => {
            let tr = $('<tr class="row"></tr>');
            tr.append($(`<td class="col-md-6"></td>`)
                .append(`<input type="text" name="Content" id="content${task.id}" value="${task.content}" />`));

            tr.append($(`<td class="col-md-2"></td>`)
                .append(`<input type="text" name="StartDate" id="startDate{task.id}" value="${NormalizeDate(task.startDate)}" />`));
            let endDateInput = $(`<input type="date" name="EndDate" id=$"endDate${task.Id}" value="" />`);
            endDateInput.val(NormalizeEndDate(task.endDate));
            tr.append($(`<td class="col-md-2"></td>`).append(endDateInput));

            let actionTd = $(`<td class="col-md-2"></td>`);
            let editBtn = $(`<button class="btn btn-primary edit" data-id="${task.id}">Edit</button>`).click(editHandler);
            actionTd.append(editBtn);
            let deleteBtn = $(`<button class="btn btn-danger delete" data-id="${task.Id}">Delete</button>`).click(deleteHandler);
            actionTd.append(deleteBtn);
            tr.append(actionTd);

            $("#tasks>tbody").append(tr);
        })
        .fail(err => {
            alertMessage(err.responseJSON);
        });
}

function editHandler(event) {
    let button = $(event.target)[0];
    let id = button.dataset.id;
    let contentElement = $("#content" + id);
    let endDateElement = $("#endDate" + id);
    let content = contentElement.val();
    let endDate = endDateElement.val();

    $.ajax({
        url: "/Task/Edit",
        type: "PUT",
        data: { id, content, endDate }
    })
    .done((task) => {
        contentElement.val(task.content);
        endDateElement.val(NormalizeEndDate(task.endDate));
    })
        .fail((err) => {
            console.log(err);
            console.log(err.responseText);
            let errorMessage = err.responseJSON;
            if (!errorMessage) {
                errorMessage = err.responseText;
            }
            alertMessage(errorMessage);
    });

}
function deleteHandler(event) {
    let button = $(event.target)[0];
    let id = button.dataset.id;

    $.ajax({
        url: "/Task/Delete/" + id,
        type: "DELETE"
    })
    .done(() => {
        $("#content" + id).parent().parent().remove();
    })
    .fail(() => {
        alertMessage("The element with such id does not exist!");
    });
}

function alertMessage(message) {
    let alertDiv = $(`<div class="alert alert-primary" role="alert"></div`);
    alertDiv.text(message);
    $('header:first').after(alertDiv);
    setTimeout(() => {
        alertDiv.remove();
    }, 4000);
}


function NormalizeDate(dateString) {
    let date = new Date(dateString);
    let day = ("0" + date.getDate()).slice(-2);
    let month = ("0" + (date.getMonth() + 1)).slice(-2);
    let year = date.getFullYear();
    return `${day}.${month}.${year}`;
}


function NormalizeEndDate(dateString) {
    let date = new Date(dateString);
    let day = ("0" + date.getDate()).slice(-2);
    let month = ("0" + (date.getMonth() + 1)).slice(-2);
    let year = date.getFullYear();
    return `${year}-${month}-${day}`;
}