function frameMsg(msg) {
    layui.use('layer', function (layer) {
        layer.msg(msg);
    })
}
function reloadTable() {
    layui.use('table', function (table) {
        table.reload('table');
    });
}
function layerOpen(options) {
    layui.use('layer', function (layer) {
        layer.open(options);
    })
}