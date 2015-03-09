function AdminViewModel(app, dataModel) {

    var self = this;
    self.requests = ko.observableArray('');
    self.currentRequest = ko.observable(null);
    self.header = ko.computed(function() {
        return self.requests().length + ' Pending requests';

    });
    self.getFormattedDate = function (date) {
        var newDate = new Date(date);
        var result = newDate.toLocaleDateString('en-us') + ' at ' + newDate.toLocaleTimeString('en-us');
        return result;
    }
    Sammy(function() {
        this.get('/Lemadmin', function() {
            $.ajax({
                method: 'post',
                url: '/LemAdmin/GetAllRequests',
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function(data) {
                    ko.utils.arrayPushAll(self.requests, data);
                }
            });
        });
    });

    $(document).ready(function() {
       
    });
    
    return self;
}
app.addViewModel({
    name: "Admin",
    bindingMemberName: "admin",
    factory: AdminViewModel
})