function AddStoryPlot() {
    var storyPlotItem = $('.story-plot').children()[1];
    var newItem = $(storyPlotItem).clone(true);
    newItem.children().each(function () {
        //نتأكد من أن العنصر هو حقل إدخال
        if (!$(this).undefined) {
            //إفراغ الحقول من أي قيمة قد تحملها
            $(this).find('.story-plot-text').val('').end();
            $(this).find('.story-plot-image').val('').end();
            $(this).find('.story-plot-sort').val('').end();
        }
    });
    newItem.appendTo('.story-plot');
    event.preventDefault();
    return false;
}
function renameFieldsId(itemFields) {
    var indexReplaceFunction = function (j) {
        var name = $(this).attr("name");
        var newName = name.substring(0, name.indexOf("[") + 1) + j + name.substring(name.indexOf("]"));
        $(this).attr("name", newName);
    };

    for (var i = 0; i < itemFields.length; i++) {
        $("." + itemFields[i]).each(indexReplaceFunction);
    }
}
function onFormSubmit() {
    var fields = ["story-plot-text", "story-plot-sort"];
    renameFieldsId(fields);
}
