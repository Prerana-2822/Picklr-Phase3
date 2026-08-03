$.validator.addMethod("atleastoneday", function (value, element) {

    return $("#Monday").is(":checked") ||
           $("#Tuesday").is(":checked") ||
           $("#Wednesday").is(":checked") ||
           $("#Thursday").is(":checked") ||
           $("#Friday").is(":checked") ||
           $("#Saturday").is(":checked") ||
           $("#Sunday").is(":checked");

});

$.validator.unobtrusive.adapters.addBool("atleastoneday");