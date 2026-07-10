define(['durandal/system', 'durandal/app'],
    function (system, app) {
        var logger = {
            log: log,
            logError: logError
        };

        return logger;

        function log(message, data, source, showToast) {
            logIt(message, data, source, showToast, 'info');
        }

        function logError(message, data, source, showToast) {
            logIt(message, data, source, showToast, 'error');
        }

        function logIt(message, data, source, showToast, toastType) {
            source = source ? '[' + source + '] ' : '';
            if (data) {
                system.log(source, message, data);
            } else {
                system.log(source, message);
            }
            if (showToast) {
                if (toastType === 'error') {
                    app.showMessage(message, 'Erro', ['Ok']);
                    //toastr.error(message);
                } else {
                    app.showMessage(message, 'Mensagem', ['Ok']);
                    //toastr.info(message);
                }

            }

        }
    });