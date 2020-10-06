$(document).ready(function () {
    $('#roomTable').DataTable();
    $('#guestsTable').DataTable();
    $('#reservationsTable').DataTable();
    $('#maintenancesTable').DataTable();
    $('#maintenanceListTable').DataTable();

    //$('#roomTable tbody').on('click', 'tr', function () {
    //    $(this).toggleClass('selected');
    //});
    
    //$('#buttonS').click(function () {
    //    var selectedRooms = $('input[type=checkbox]:checked').map(function (_, el) {
    //        return $(el).val();
    //    }).get();

    //    document.getElementById("inputSelectedRooms").value = selectedRooms;

    //    alert(selectedRooms);
    //});
    
});