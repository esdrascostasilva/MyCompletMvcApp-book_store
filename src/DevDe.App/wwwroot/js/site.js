function AjaxModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                    keyboard: true
                                },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide'); // If sucess, hide my modal and...
                            $('#AddressTarget').load(result.url); //Load the result html for the div 
                        } else {
                            $('#myModalContent').hmtl(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }

    });
}

function SearchZipCode() {
    $(document).ready(function () {

        function clear_form_zipcode() {
            // Clean Zip Code's value form
            $("#Address_Street").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        // When the field zipcode lost focus
        $("#Address_ZipCode").blur(function () {
            // new variable "zipcode" only numbers
            var zipcode = $(this).val().replace(/\D/g, '');

            // Check if the field is valid
            if (zipcode != "") {
                // Regular expression to valid Zip Code
                var validzipcode = /^[0-9]{8}$/;

                // Valid the Zip Code format
                if (validzipcode.test(zipcode)) {
                    // fill the fields when consult webservice
                    $("#Address_Street").val("...");
                    $("#Address_Neighborhood").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    // Consult thew webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + zipcode + "/json/?callback=?",
                        function (data) {
                            if (!("erro" in data)) {
                                // Update the fields with consult's values
                                $("#Address_Street").val(data.logradouro);
                                $("#Address_Neighborhood").val(data.bairro);
                                $("#Address_City").val(data.localidade);
                                $("#Address_State").val(data.uf);
                            }
                            else {
                                // Zip Code not found
                                clear_form_zipcode();
                                alert("CEP não encontrado");
                            }
                        });
                }
                else {
                    clear_form_zipcode();
                }
            }
        });
    });
    
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
}); 