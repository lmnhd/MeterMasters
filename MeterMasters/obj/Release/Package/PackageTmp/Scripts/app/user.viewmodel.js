
function MixRequest() {
    var self = this;
    
    self.VersionType = ko.observableArray();
    self.id = ko.observable(0);
    self.clientId = ko.observable(0);
    self.clientUserId = ko.observable(0);
    self.title = ko.observable('ttttt');
    self.uploadLink = ko.observable('');
    self.canModifySounds = ko.observable(false);
    self.genre = ko.observable('');
    self.modifyNotes = ko.observable('');
    self.clientNotes = ko.observable('');
    self.engineerNotes = ko.observable('');
    self.entryTime = ko.observable(new Date());
    self.ArchiveType = ko.observable(1);
    self.DAW = ko.observable(1);
    self.acceptanceTime = ko.observable(new Date());
    self.MixTypeChoices = ko.observableArray(['FullTracks', 'VocalsOnly', 'TrackOnly']);
    self.myMixType = ko.observable(0);
    self.VersionTypes = ko.observableArray(['Instrumental', 'Acapella', 'ShowVersion', 'Special']);
    
    self.ChosenVersions = ko.observableArray();
    self.ChosenMixTypes = ko.observableArray();

    self.setClientId = function(mclientId) {
        self.clientId = mclientId;
    }
    self.setClientUserId = function(muserId) {
        self.clientUserId = muserId;
    }

}

function UserViewModel(app, dataModel) {
    var self = this;

    self.userName = ko.observable('');
    self.message = ko.observable('unknown.');
    self.myHomeTown = ko.observable('');
    self.myEmail = ko.observable('');
    self.userID = ko.observable('');
    self.clientID = ko.observable('');
    self.mixRequests = ko.observableArray();
    self.currentRequest = ko.observable(new MixRequest());

    self.createMixRequest = function() { self.currentRequest(new MixRequest()); }
    
    self.getMixType = function (type) {
        switch (type) {
            case 0:
                return ("All Instruments and vocals");

            case 1:
                return ("Vocals only");
            case 2:
                return ("Instruments Only");

            default:
                return ("");
        }
    }
    self.findRequest = function(id) {
        for (var i = 0; i<self.mixRequests().length; i++) {
            var req = self.mixRequests()[i];
            if (req.id == id) {
                return req;
            }
        }
    }
    self.cancelRequest = function(req) {
        var request = req;
        if (request) {
            $('#cancelled-mix-title').text(request.title);
            $('#cancel-request-modal').modal('show');
            $('#confirm-cancel-request').on('click', function () {
                $('#cancel-request-modal').modal('hide');
                var data = JSON.stringify(request);
                $.ajax({
                    method: 'post',
                    url: 'api/Me/CancelRequest',
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    headers: {
                        'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                    },
                    success: function (data) {
                        if(data)
                            self.mixRequests.remove(request);
                        var msg = request.title + ' has been cancelled';
                        self.DisplayGeneralModal('Request cancelled',msg);

                    }
                });

            });
        }
    }
    self.canModifySounds = function(value) {
        if (value) {
            return 'Yes';
        }
        return 'No';
    }
    self.getFormattedDate = function(date) {
        var newDate = new Date(date);
        var result = newDate.toLocaleDateString('en-us') + ' at ' + newDate.toLocaleTimeString('en-us');
        return result;
    }
    self.DisplayGeneralModal = function(header,message) {
        $('#general-info-modal').find('.modal-body').find('p').first().text(message);
        $('#general-info-modal').find('.modal-header').find('h4').first().text(header);
        $('#general-info-modal').modal('show');
    }
    Sammy(function() {

        this.get('#user', function() {

            var thisapp = this.app;
            if (!dataModel.getAccessToken()) {
                // The following code looks for a fragment in the URL to get the access token which will be
                // used to call the protected Web API resource
                var fragment = common.getFragment();

                if (fragment.access_token) {
                    // returning with access token, restore old hash, or at least hide token
                    alert(fragment.access_token);
                    window.location.hash = fragment.state || '';
                    dataModel.setAccessToken(fragment.access_token);
                } else {
                    //no token - so bounce to Authorize endpoint in AccountController to sign in or register
                    window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
                }
            } else {
                $.ajax({
                    method: 'get',
                    url: app.dataModel.clientInfoUrl,
                    contentType: "application/json; charset=utf-8",
                    headers: {
                        'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                    },
                    success: function (data) {
                        self.userName(data.userName);
                        self.myHomeTown(data.hometown);
                        self.myEmail(data.email);
                        self.userID(data.userId);
                        self.clientID(data.clientId);
                        if (data.requests) {
                            ko.utils.arrayPushAll(self.mixRequests, data.requests);
                        }
                       
                        //self.currentRequest().setClientUserId(data.userId);
                        //self.currentRequest().setClientId(data.clientId);

                        thisapp.runRoute('get', '#user/' + self.userName());
                        //self.myHometown('Your Hometown is : ' + data.hometown);
                        //self.welcomeMessage("Welcome " + data.userName);

                    }
                });
                
            }

        	
        });
        this.get('#user/:name', function() {
            $('#info-container').hide();
            $('#main-content').hide();
            $('#user-content').show();

            
            //var name = this.params['name'];
            //alert(name);

        });

        this.post("#/UpdateInfo", function () {
            $('#info-container').hide();

            var formData = $("#client-info").serialize();
            $.ajax({
               method: 'post',
               url: 'api/Me/Update',
                data:formData,
               contentType: "application/x-www-form-urlencoded; charset=utf-8",
                headers: {
                   'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
               },
               success: function(data) {
                    //alert(data);
                }
            });

           
        });

        this.post("#/submitmix", function() {
            var formData = $('#request-info').serialize();
            $.ajax({
                method: 'post',
                url: 'api/Me/SubmitMix',
                data: formData,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function(data) {
                    self.mixRequests.push(data);
                    self.createMixRequest();
                    $('#pending-requests').collapse('show');
                    $('#submit').collapse('hide');
                    $('#post-request-modal').modal('show');
                }
            });
        });

    });
    return self;

}

app.addViewModel({
    name: "User",
    bindingMemberName: "user",
    factory: UserViewModel
});