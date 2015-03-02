function HomeViewModel(app) {
    var self = this;

    
    self.myHometown = ko.observable("");
    self.currentID = ko.observable("");
    self.welcomeMessage = ko.observable("");
    self.test = function() {
        alert("works!!");
    }
    self.showInfo = function(data, infodiv) {
        //var btn = $(event.currentTarget);
        var name = '#' + data;
        var par = $(name);
        var meters = par.next('div');
        
        
        self.currentID(data);
        $('#main-content').fadeOut(500, function() {
            infodiv.fadeIn(500);
            
        });
        //par.removeClass('visible').addClass('invisible');
        //meters.removeClass('visible').addClass('invisible');
        //info.removeClass('invisible').addClass('visible');
        //meters.fadeOut(500, function () {
        //    par.fadeOut(500, function () {
        //       info.fadeIn(500);
        //    });
        //});


    }
    $.localScroll.defaults.axis = 'xy';
    /**
    * NOTE: I use $.localScroll instead of $('#navigation').localScroll() so I
    * also affect the >> and << links. I want every link in the page to scroll.
    */
    $.localScroll({
        target: '#main-content', // could be a selector or a jQuery object too.
        queue: true,
        duration: 1000,
        hash: true,
        onBefore: function (e, anchor, $target) {
            // The 'this' is the settings object, can be modified
        },
        onAfter: function (anchor, settings) {
            // The 'this' contains the scrolled element (#content)
        }
    });
    Sammy(function () {
        this.get('#home', function () {
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $('#info-container').hide();
            $('#user-content').hide();

            $('#main-content').show();

            var id = '#' + self.currentID();
            var currentEl = $(id);
            var offset = currentEl.offset();
            $(document.body).scrollTo(offset.top - 100);
            //$('#main-content').animate({ scrollTop: 2000 });
            
        });
        this.get('/', function () { this.app.runRoute('get', '#home'); });

        this.get('#/info/:id', function () {
           

            var id = this.params['id'];
            var infoID = '#' + id + '-info';


            $('#moreInfo').load('/scripts/JsonData/info.html ' +  infoID ,function() {
                var info = $('#info-container');

                var infodiv = info.find('.infotext').first();
                var header = infodiv.data('h2');
                $('#info-header').text(header);
                self.showInfo(id, info);
            });
            
            
        });
    });

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
