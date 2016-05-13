(function ($) {
    function Transaction() {
        var $thisthis = this;
        function initializeAddEditTransaction() {
            $('.datepicker').datepicker({
                "setDate": new Date(),
                "autoclose": true
            });
        }
        $this.init = function () {
            initializeAddEditTransaction();
        }
    }
    $(function () {
        var self = new Transaction();
        self.init();
    });
}(jQuery))