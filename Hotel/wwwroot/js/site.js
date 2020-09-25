$(document).ready(function () {
    var table = $('#roomTable').DataTable(
        {
            select: true,
            select: {
                style: 'multi'
            }
        }
    );

    $('#roomTable tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });
    
    $('#buttonS').click(function () {
        var selectedRooms = $('input[type=checkbox]:checked').map(function (_, el) {
            return $(el).val();
        }).get();

        document.getElementById("inputSelectedRooms").value = selectedRooms;

        alert(selectedRooms);
    });
    var tableReservations = $('#reservationsTable').DataTable(
        {
            select: true,
            select: {
                style: 'multi'
            }
        }
    );
});