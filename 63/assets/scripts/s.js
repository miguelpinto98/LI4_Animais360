var p1 = new google.maps.LatLng(44.5403, 20);
var p2 = new google.maps.LatLng(44.5403, 50);
var p3 = new google.maps.LatLng(44.54,75);

var mapOptions = {
        center: p1, zoom: 2, 
        mapTypeId: google.maps.MapTypeId.TERRAIN,
        panControl: false, zoomControl: false, mapTypeControl: false,
        scaleControl: false, streetViewControl: false,
        //overviewMapControl: boolean
    };


var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);



function initialize() {
    var marker = new google.maps.Marker({position: p1, map: map, icon:'http://maps.google.com/mapfiles/ms/icons/blue.png', title: 'Clique para info'});
    var marker2 = new google.maps.Marker({position: p2,map: map, title: 'Clique para zoom'});

    var info = '<div id="content">'+'<div id="siteNotice">'+'Braga, Portugal'+'</div>'+
      '<h1 id="firstHeading" class="firstHeading">Gerês</h1>'+
      '<div id="bodyContent">'+'<p>Esta área é muito <b>importante</b> porque sim.</p>'+'</div>'+'</div>';

    var infoWindow = new google.maps.InfoWindow({content: info});

    //Função que muda a "camara" para o ponto marcado, 3 segundos depois de mudar de sítio.
    /*google.maps.event.addListener(map, 'center_changed', function(){window.setTimeout( function(){map.panTo(marker.getPosition());}, 3000);});*/


    //vermelho
    google.maps.event.addListener( marker2, 'click', function(){document.getElementById('dialog-anchor').click();});


    //Fazer info no make
    google.maps.event.addListener(marker, 'click', function() {infoWindow.open(map,marker);});


    var flightPlanCoordinates = [
        p1,p2,p3
    ];

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
//google.maps.event.addDomListener(window, 'load', function(){document.getElementById('dialog-anchor').click();});


if (typeof countitroundinstance === "undefined") {var countitroundinstance = [];}
        countitroundinstance.push({"unique":"countitround_LTSGPIDRAB","startdate":"0","enddate":"30","now":"0","color4":"#F39C12","backgroundcolor4":"#34495E","glowwidth4":"0","backgroundwidth4":"1","frontwidth4":"4","size4":"30"});
    