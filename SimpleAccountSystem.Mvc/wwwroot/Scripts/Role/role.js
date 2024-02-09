$(document).ready(() => {

    const table = new DataTable('#role-table', {

        //pagination settings
        pagingType: 'full_numbers',

        // server side processing settings
        ajax: {
            url: `Role/GetRoles`,
            type: 'GET',
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        },
        processing: true,
        serverSide: true,

        // columns settings
        columns: [
            { data: "Name" },
            { data: "ConcurrencyStamp" },
            {
                data: "actions",
                render: (data, type) => {
                    return "<btn class = 'btn btn-danger'>Remove</btn>";
                }
            },
        ]

    });

});