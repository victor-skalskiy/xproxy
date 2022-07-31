$(document).ready(function () {
    setMultiselect('RegionIds', 'region-select2-values')
    setMultiselect('SourceIds', 'source-select2-values')
});

function setMultiselect(id, valuesName) {
    const sel = $('#' + id);
    sel.select2();
    sel.val(getArrayValue(valuesName))
    console.log(getArrayValue(valuesName))
    sel.trigger('change')
}

function getArrayValue(name) {
    const str = $('#' + name).val();
    if (!str)
        return [];
    
    return str.split(',');
}