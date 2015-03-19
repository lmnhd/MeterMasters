function AdminViewModel(app, dataModel) {

    var self = this;
    self.requests = ko.observableArray('');
    self.currentRequest = ko.observable(null);
    self.header = ko.computed(function() {
        return self.requests().length + ' Pending requests';


    });
    self.accepted = function (request) {
        
        return request.acceptanceTime == request.entryTime;
    };
    self.setCurrentRequest = function(request) {
        self.currentRequest(request);
    }
    self.acceptRequest = function(request) {
        var endPoint = 'api/Me/acceptRequest';
        var success = function(data) {
            
        }
        self.postToServer(endPoint,JSON.stringify(request), success);
    }
    self.getFormattedDate = function (date) {
        var newDate = new Date(date);
        var result = newDate.toLocaleDateString('en-us') + ' at ' + newDate.toLocaleTimeString('en-us');
        return result;
    }
    self.postToServer = function(endPoint,data, success) {
        $.ajax({
            method: 'post',
            url: endPoint,
            data: data,
            contentType: "application/json; charset=utf-8",
            headers: {
                'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
            },
            success: success
            
        });
    }
    Sammy(function() {
        this.get('/Lemadmin', function() {
            $.ajax({
                method: 'post',
                url: 'api/Me/GetAllRequests',
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function(data) {
                    ko.utils.arrayPushAll(self.requests, data);
                    if (self.requests().length > 0) {
                        self.currentRequest(self.requests[0]);
                    }
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