$(document).ready(() => {

    const table = new DataTable('#user-table', {

        //pagination settings
        pagingType: 'full_numbers',

        // server side processing settings
        ajax: {
            url: `User/GetUsers`,
            type: 'GET',
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        },
        processing: true,
        serverSide: true,

        // columns settings
        columns: [
            { data: "userName" },
            { data: "email" },
            {
                data: "twoFactorEnabled",
                render: (data, type) => {
                    if (data)
                        return "enabled";
                    else
                        return "disabled";
                }
            },
            {
                data: "actions",
                render: (data, type) => {
                    return "<btn class = 'btn btn-danger'>Remove</btn>";
                }
            },
        ]

    });

    $('#btn-add-user').on('click', () => {
        $('#user-modal').modal('show');
    });

});