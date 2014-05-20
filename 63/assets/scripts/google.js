
function initialize() {
    var myLatlng = new google.maps.LatLng(44.5403, 20);
    var myLatlng2 = new google.maps.LatLng(44.5403, 50);


    var map_canvas = document.getElementById('map_canvas');
    var map_options = {center: myLatlng, zoom: 2};
    var map = new google.maps.Map(map_canvas, map_options);

    var marker = new google.maps.Marker({position: myLatlng,map: map, title: 'Clique para info'});

    var marker2 = new google.maps.Marker({position: myLatlng2,map: map, title: 'Clique para zoom'});

    var contentString = 
      '<div id="content">'+
        '<div id="siteNotice">'+
            'asdasjd'+
        '</div>'+
            '<h1 id="firstHeading" class="firstHeading">Area, Pais</h1>'+
                '<div id="bodyContent">'+
                    '<p>A <b>63</b> é linda</p>'+
                    '<p>Attribution: Uluru, '+
                        '<a href="http://en.wikipedia.org"</a>'+
                        '(last visited June 22, 2009).'+
                    '</p>'+
                '</div>'+
      '</div>';

       var infowindow = new google.maps.InfoWindow({content: contentString});

    //Função que muda a "camara" para o ponto marcado, 3 segundos depois de mudar de sítio.
    /*google.maps.event.addListener(
        map, 'center_changed', function(){
            window.setTimeout( function(){map.panTo(marker.getPosition());}, 3000);
        }
    );*/


    //Fazer zoom no marker2
    google.maps.event.addListener(
        marker2, 'click', function(){
            map.setZoom(8);
            map.setCenter(marker2.getPosition());
        }
    );

    //Fazer info no make
    google.maps.event.addListener(
        marker, 'click', function() {
            infowindow.open(map,marker);
        }
    );
}

google.maps.event.addDomListener(window, 'load', initialize);










(function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
              function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
              e=o.createElement(i);r=o.getElementsByTagName(i)[0];
              e.src='//www.google-analytics.com/analytics.js';
              r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
              ga('create','UA-48454066-1');ga('send','pageview');
