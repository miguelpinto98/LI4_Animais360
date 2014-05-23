//pontos
var p1= new google.maps.LatLng(44.5403, 20);
var p2= new google.maps.LatLng(44.5403, 50);


var map_canvas = document.getElementById('map_canvas');
var map_options = {
    center: p1, zoom: 2,
    panControl: false,
    zoomControl: false,
    mapTypeControl: false,
    scaleControl: false,
    streetViewControl: false,
    //overviewMapControl: boolean
};

//MAPA
var map = new google.maps.Map(map_canvas, map_options);


function HomeControl(controlDiv, map) {

  controlDiv.style.padding = '5px';

  var caixa = document.createElement('div');
    caixa.style.width='200px';
    caixa.style.minHeight='5px';
    caixa.style.borderStyle='solid';
    caixa.style.borderColor='#E0E4E8';
    caixa.style.borderWidth='2px';
    caixa.style.textAlign = 'center';

 

  var header = document.createElement('div');
    header.style.width='196px';
    header.style.height='30px';
    header.style.borderStyle='solid';
    header.style.borderColor='#E0E4E8';
    header.style.borderWidth='0px 0px 2px 0px';
    header.style.backgroundColor='#F39C12';
    header.style.textAlign = 'center';



  var titulo = document.createElement('div');
    titulo.style.fontFamily = 'Helvetica';
    titulo.style.fontSize = '14px';
    titulo.style.color = '#fff';
    titulo.style.paddingLeft = '6px';
    titulo.style.paddingRight = '4px';
    titulo.style.paddingTop= '4px';
    titulo.innerHTML = '<b>Próximos Destinos</b>';


  var content = document.createElement('div');
    content.style.width='196px';
    content.style.backgroundColor='#fff';
    content.style.textAlign = 'center';


    var esp = document.createElement('div');
        esp.style.width='196px';
        esp.style.height='10px';
        esp.style.backgroundColor='#fff';

    controlDiv.appendChild(caixa);
    caixa.appendChild(header);
    header.appendChild(titulo);
    caixa.appendChild(content);
    content.appendChild(esp);

    for(i=0;i<2;i++){

          var ap = document.createElement('div');
            ap.style.width='180px';
            ap.style.height='25px';
            ap.style.backgroundColor='#fff';
            ap.style.marginLeft = '10px';
            ap.style.cursor='pointer';
            ap.title = 'Ir para...';

            
         var apT = document.createElement('div');
            apT.style.fontFamily = 'Helvetica';
            apT.style.fontSize = '11px';
            apT.style.color = 'gray';
            apT.style.paddingLeft = '6px';
            apT.style.paddingRight = '4px';
            apT.style.paddingTop= '4px';
            apT.innerHTML = '<b>Geres, Braga, Portugal</b>';

           
            content.appendChild(ap);
            ap.appendChild(apT);
    }





  google.maps.event.addDomListener(ap, 'click', function() {
    map.setCenter(p2); map.setZoom(8);
  });

   google.maps.event.addDomListener(ap, 'click', function() {
    map.setCenter(p2); map.setZoom(8);
  });


}




function initialize() {
    
    var marker, marker2;
    var info, infoW;

    marker = new google.maps.Marker({position: p1, map: map, icon:'http://maps.google.com/mapfiles/ms/icons/blue.png', title: 'Clique para info'});
    marker2 = new google.maps.Marker({position: p2,map: map, title: 'Clique para zoom'});

    info = 
      '<div id="content">'+'<div id="siteNotice">'+'asdasjd'+'</div>'+
      '<h1 id="firstHeading" class="firstHeading">Area, Pais</h1>'+
      '<div id="bodyContent">'+'<p>A <b>63</b> é linda</p>'+
      '<p>Attribution: Uluru, '+'<a href="http://en.wikipedia.org"</a>'+
      '(last visited June 22, 2009).'+'</p>'+'</div>'+'</div>';

    infoWindow = new google.maps.InfoWindow({content: info});

    //Função que muda a "camara" para o ponto marcado, 3 segundos depois de mudar de sítio.
    /*google.maps.event.addListener(
        map, 'center_changed', function(){
            window.setTimeout( function(){map.panTo(marker.getPosition());}, 3000);
        }
    );*/


    //vermelho
    google.maps.event.addListener(
        marker2, 'click', function(){
           document.getElementById('dialog-anchor').click();
        }
    );


    //Fazer info no make
    google.maps.event.addListener(
        marker, 'click', function() {
            infoWindow.open(map,marker);
        }

    );



    //CAIXA
    var homeControlDiv = document.createElement('div');
    var homeControl = new HomeControl(homeControlDiv, map);
    homeControlDiv.index = 1;
    map.controls[google.maps.ControlPosition.TOP_RIGHT].push(homeControlDiv);

}




google.maps.event.addDomListener(window, 'load', initialize);


(function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
    function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
    e=o.createElement(i);r=o.getElementsByTagName(i)[0];
    e.src='//www.google-analytics.com/analytics.js';
    r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
    ga('create','UA-48454066-1');ga('send','pageview');
