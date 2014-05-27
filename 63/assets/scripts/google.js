var p1 = new google.maps.LatLng(44.5403, 20);
var p2 = new google.maps.LatLng(44.5403, 50);
var p3 = new google.maps.LatLng(44.54,75);


var map_options = {
        center: p1, zoom: 2, 
        mapTypeId: google.maps.MapTypeId.TERRAIN,
        panControl: false, zoomControl: false, mapTypeControl: false,
        scaleControl: false, streetViewControl: false,
        //overviewMapControl: boolean
};


var map = new google.maps.Map(document.getElementById('map_canvas'), map_options);




function initialize() {
    var azul = new google.maps.Marker({position: p1, map: map, icon:'http://maps.google.com/mapfiles/ms/icons/blue.png', title: 'Clique para info'});
    var vermelho = new google.maps.Marker({position: p2,map: map, title: 'Clique para zoom'});

    var contentString = 
      '<div id="content">'+'<div id="siteNotice">'+'asdasjd'+'</div>'+
      '<h1 id="firstHeading" class="firstHeading">Area, Pais</h1>'+
      '<div id="bodyContent">'+'<p>A <b>63</b> é linda</p>'+
      '<p>Attribution: Uluru, '+'<a href="http://en.wikipedia.org"</a>'+
      '(last visited June 22, 2009).'+'</p>'+'</div>'+'</div>';

    var infowindow = new google.maps.InfoWindow({content: contentString});

    //Função que muda a "camara" para o ponto marcado, 3 segundos depois de mudar de sítio.
    /*google.maps.event.addListener(
        map, 'center_changed', function(){
            window.setTimeout( function(){map.panTo(marker.getPosition());}, 3000);
        }
    );*/


    //Fazer zoom no vermelho
    google.maps.event.addListener(
        vermelho, 'click', function(){
            map.setZoom(8);
            map.setCenter(vermelho.getPosition());
        }
    );

    //Janela de info no azul
    google.maps.event.addListener(
        azul, 'click', function() {
            infowindow.open(map,azul);
        }
    );



    var flightPlanCoordinates = [p1,p2,p3];

    var flightPath = new google.maps.Polyline({
        path: flightPlanCoordinates,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

  flightPath.setMap(map);
}


google.maps.event.addDomListener(window, 'load', initialize);



//CENAS



/*
(function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
  function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
  e=o.createElement(i);r=o.getElementsByTagName(i)[0];
  e.src='//www.google-analytics.com/analytics.js';
  r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
  ga('create','UA-48454066-1');ga('send','pageview');*/




