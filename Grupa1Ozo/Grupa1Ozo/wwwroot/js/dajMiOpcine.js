$.ajax({
    url: "/Zaposlenici/DajMiOpcine", success: function (result) {
        const opcine = result;
        console.log(opcine);

        for (var i = 0; i < opcine.length; i++) {
            $("select").append("<option value=" + opcine[i].opcinaId +">" + opcine[i].naziv + "</option>");
        }
    }
});