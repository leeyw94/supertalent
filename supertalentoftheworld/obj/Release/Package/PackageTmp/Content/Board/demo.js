type = ['', 'info', 'success', 'warning', 'danger'];


demo = {   
    initPickColor: function() {
        $('.pick-class-label').click(function() {
            var new_class = $(this).attr('new-class');
            var old_class = $('#display-buttons').attr('data-class');
            var display_div = $('#display-buttons');
            if (display_div.length) {
                var display_buttons = display_div.find('.btn');
                display_buttons.removeClass(old_class);
                display_buttons.addClass(new_class);
                display_div.attr('data-class', new_class);
            }
        });
    },    

    showNotification: function(from, align) {
        color = Math.floor((Math.random() * 4) + 1);

        $.notify({
                icon: "pe-7s-gift",
                message: "<b>Light Bootstrap Dashboard PRO</b> - forget about boring dashboards."
            }, {
                type: type[color],
                timer: 3000,
                placement: {
                    from: from,
                    align: align
                }
            });
    },
    initCharts: function() {

        /*  **************** 24 Hours Performance - single line ******************** */

        var dataPerformance = {
            labels: ['6pm', '9pm', '11pm', '2am', '4am', '8am', '2pm', '5pm', '8pm', '11pm', '4am'],
            series: [
                [1, 6, 8, 7, 4, 7, 8, 12, 16, 17, 14, 13]
            ]
        };

        var optionsPerformance = {
            showPoint: false,
            lineSmooth: true,
            height: "160px",
            axisX: {
                showGrid: false,
                showLabel: true
            },
            axisY: {
                offset: 40,
            },
            low: 0,
            high: 26
        };

        Chartist.Line('#chartPerformance', dataPerformance, optionsPerformance);


        /*  **************** NASDAQ: AAPL - single line with points ******************** */

        var dataStock = {
            labels: ['\'07', '\'08', '\'09', '\'10', '\'11', '\'12', '\'13', '\'14', '\'15'],
            series: [
                [22.20, 34.90, 42.28, 51.93, 62.21, 80.23, 62.21, 82.12, 102.50, 107.23]
            ]
        };

        var optionsStock = {
            lineSmooth: false,
            height: "160px",
            axisY: {
                offset: 40,
                labelInterpolationFnc: function(value) {
                    return '$' + value;
                }
            },
            low: 10,
            high: 110,
            classNames: {
                point: 'ct-point ct-white',
                line: 'ct-line ct-white'	           
            }
        };

        Chartist.Line('#chartStock', dataStock, optionsStock);	 
	

        /*  **************** Views  - barchart ******************** */

        var dataViews = {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            series: [
                [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]
            ]
        };

        var optionsViews = {
            seriesBarDistance: 10,
            classNames: {
                bar: 'ct-bar ct-white'
            },
            axisX: {
                showGrid: false
            }
        };

        var responsiveOptionsViews = [
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function(value) {
                        return value[0];
                    }
                }
            }]
        ];

        Chartist.Bar('#chartViews', dataViews, optionsViews, responsiveOptionsViews);


    },

    initFullScreenGoogleMap: function() {
        var myLatlng = new google.maps.LatLng(40.748817, -73.985428);
        var mapOptions = {
            zoom: 13,
            center: myLatlng,
            scrollwheel: false, //we disable de scroll over the map, it is a really annoing when you scroll through page
            styles: [{ "featureType": "water", "stylers": [{ "saturation": 43 }, { "lightness": -11 }, { "hue": "#0088ff" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "hue": "#ff0000" }, { "saturation": -100 }, { "lightness": 99 }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "color": "#808080" }, { "lightness": 54 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "color": "#ece2d9" }] }, { "featureType": "poi.park", "elementType": "geometry.fill", "stylers": [{ "color": "#ccdca1" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#767676" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b8cb93" }] }, { "featureType": "poi.park", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.medical", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "stylers": [{ "visibility": "simplified" }] }]
        };
        var map = new google.maps.Map(document.getElementById("map"), mapOptions);

        var marker = new google.maps.Marker({
            position: myLatlng,
            title: "Hello World!"
        });

        // To add the marker to the map, call setMap();
        marker.setMap(map);
    },

    initVectorMap: function() {
        var mapData = {
            "AU": 760,
            "BR": 10,
            "CA": 120,
            "DE": 1300,
            "FR": 540,
            "GB": 690,
            "GE": 200,
            "IN": 200,
            "RO": 600,
            "RU": 300,
            "US": 2920,
        };
        var plants = [
            { name: 'VAK', coords: [50.0091294, 9.0371812], status: 'closed', offsets: [0, 2] }
            //{ name: 'MZFR', coords: [49.0543102, 8.4825862], status: 'closed', offsets: [0, 2] },

   //{ name: 'KWO', coords: [49.364503, 9.076252], status: 'closed' },
        ];
        $('#worldMap').vectorMap({
            map: 'world_mill_en',
            backgroundColor: "transparent",
            zoomOnScroll: false,
            regionStyle: {
                initial: {
                    fill: '#e4e4e4',
                    "fill-opacity": 0.9,
                    stroke: 'none',
                    "stroke-width": 0,
                    "stroke-opacity": 0
                }
            },
            markers: plants.map(function(h) { return { name: h.name, latLng: h.coords }; }),
            labels: {
                markers: {
                    render: function(index) {
                        return plants[index].name;
                    },
                    offsets: function(index) {
                        var offset = plants[index]['offsets'] || [0, 0];

                        return [offset[0] - 7, offset[1] + 3];
                    }
                }
            },

            series: {
                markers: [{
                        attribute: 'fill',
                        scale: ['#C8EEFF', '#0071A4'],
                        normalizeFunction: 'polynomial',
                        values: [408, 512, 550, 781],
                        legend: {
                            vertical: true
                        }
                    },
                    {
                        attribute: 'text',
                        scale: {
                            company: ''                          
                        },
                        values: {
                            '4': 'company'                          
                        },
                        legend: {
                            horizontal: true,
                            cssClass: 'jvectormap-legend-icons',
                            title: 'Business '
                        }
                    }
                ],
                markerStyle: {
                    initial: {
                        fill: '#4DAC26'
                    },
                    selected: {
                        fill: '#CA0020'
                    },
                    hover: {
                        "fill-opacity": 0.8,
                        cursor: 'pointer'
                    },
                },
                regionStyle: {
                    initial: {
                        fill: '#B8E186'
                    },                       

                    selected: {
                        fill: '#F4A582',
                        cursor: 'pointer'
                    }
                },

                regions: [{
                        values: mapData,
                        scale: ["#AAAAAA", "#444444"],
                        normalizeFunction: 'polynomial',
                    }
                ]
            },
            onMarkerTipShow: function(event, label, index) {

                label.html(
                    '<b>' + plants[index].name + ' / state : ' + plants[index].status
                );
            },
            onRegionTipShow: function(e, el, code) {
                el.html(el.html() + ' (pay ' + mapData[code] + ')');
            }
        });
    },

    initAnimationsArea: function() {
        $('.animationsArea .btn').click(function() {
            animation_class = $(this).data('animation-class');

            $parent = $(this).closest('.animationsArea');

            $parent.find('.btn').removeClass('btn-fill');

            $(this).addClass('btn-fill');

            $parent.find('.animated')
                .removeAttr('class')
                .addClass('animated')
                .addClass(animation_class);

            $parent.siblings('.header').find('.title small').html('class: <code>animated ' + animation_class + '</code>');
        });
    },
    showSwal: function(type, _a) {
        if (type == 'basic') {
            swal("Here's a message!");

        } else if (type == 'title-and-text') {
            swal("Here's a message!", "It's pretty, isn't it?");
        } else if (type == 'success-message') {
            swal("Good job!", "You clicked the button!", "success");
        } else if (type == 'warning-message-and-confirmation') {
            swal({
                    title: "Are you sure?",
                    text: "You will not be able to recover this imaginary file!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn btn-info btn-fill",
                    confirmButtonText: "Yes, delete it!",
                    cancelButtonClass: "btn btn-danger btn-fill",
                    closeOnConfirm: false,
                }, function() {
                    swal("Deleted!", "Your imaginary file has been deleted.", "success");
                });

        } else if (type == 'warning-message-and-cancel') {
            swal({
                    title: "Are you sure?",
                    text: "You will not be able to recover this imaginary file!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonText: "Yes, delete it!",
                    cancelButtonText: "No, cancel plx!",
                    closeOnConfirm: false,
                    closeOnCancel: false
                }, function(isConfirm) {
                    if (isConfirm) {
                        swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    } else {
                        swal("Cancelled", "Your imaginary file is safe :)", "error");
                    }
                });

        } else if (type == 'custom-html') {
            swal({
                title: 'HTML example',
                html:
                    'You can use <b>bold text</b>, ' +
                        '<a href="http://github.com">links</a> ' +
                        'and other HTML tags'
            });

        } else if (type == 'auto-close') {
            swal({
                type: "warning",
                title: "Duplicate id",
                text: "Please enter your ID again.",
                timer: 1500,
                showConfirmButton: false
            });
        } else if (type == 'id-check') {
            swal({
                type: "warning",
                title: "Check your id",
                text: "Please check your id again.",
                timer: 1500,
                showConfirmButton: false
            });
        } else if (type == 'photo_view') {

            swal({	         
                html: '<script>$("#sweet-spacer").css("display","none"); </script><img src="http://3dp.theblueeye.com' + _a + '" style="max-width:1024px;width:100%;position:relative;top:0" />',	         
            });
        }
    },

    initFormExtendedSliders: function() {

        // Sliders for demo purpose in refine cards section
        if ($('#slider-range').length != 0) {
            $("#slider-range").slider({
                range: true,
                min: 0,
                max: 500,
                values: [75, 300],
            });
        }
        if ($('#refine-price-range').length != 0) {
            $("#refine-price-range").slider({
                range: true,
                min: 0,
                max: 999,
                values: [100, 850],
                slide: function(event, ui) {
                    min_price = ui.values[0];
                    max_price = ui.values[1];
                    $(this).siblings('.price-left').html('&euro; ' + min_price);
                    $(this).siblings('.price-right').html('&euro; ' + max_price);
                }
            });
        }

        if ($('#slider-default').length != 0 || $('#slider-default2').length != 0) {
            $("#slider-default, #slider-default2").slider({
                value: 70,
                orientation: "horizontal",
                range: "min",
                animate: true
            });
        }
    },

    initFormExtendedDatetimepickers: function() {
        $('.datetimepicker').datetimepicker({
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });

        $('.datepicker').datetimepicker({
            format: 'MM/DD/YYYY',
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });

        $('.timepicker').datetimepicker({
//          format: 'H:mm',    // use this format if you want the 24hours timepicker
            format: 'h:mm A',    //use this format if you want the 12hours timpiecker with AM/PM toggle
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });
    }
};