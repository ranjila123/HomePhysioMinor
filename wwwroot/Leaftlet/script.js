const x = document.getElementById("demo");

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}
function showPosition(position) {
    //x.innerHTML = "Latitude: " + position.coords.latitude +
    //    "<br>Longitude: " + position.coords.longitude;


    //map initialization
    var map = L.map('map').setView([position.coords.latitude, position.coords.longitude], 20);


    //osm layer
    var osm = new L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    });
    osm.addTo(map);



    //google street tile layer
    googleStreets = L.tileLayer('http://{s}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
    });
    //googleStreets.addTo(map);


    //google satellite tile layer
    googleSat = L.tileLayer('http://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}', {
        maxZoom: 20,
        subdomains: ['mt0', 'mt1', 'mt2', 'mt3']
    });
    //googleSat.addTo(map);

    //markericn
    var myIcon = L.icon({
        iconUrl: 'red_marker.png',
        iconSize: [38, 38],
    });

    // L.marker([27.6995705,85.306451]
    //marker
    var singleMarker = L.marker([position.coords.latitude, position.coords.longitude], {
        draggable: true
    }).addTo(map);

    //var singleMarker = L.marker([27.6995705,85.306451],{icon: myIcon, draggable:true}).addTo(map);
    //var popup = singleMarker.bindPopup('This is the nepal.'+singleMarker.getLatLng());
    //singleMarker.openPopup();
    //popup.addTo(map);
    document.getElementById('latitude').value = position.coords.latitude;
    document.getElementById('longitude').value = position.coords.longitude;

    singleMarker.on('dragend', function (e) {
        document.getElementById('latitude').value = singleMarker.getLatLng().lat;
        document.getElementById('longitude').value = singleMarker.getLatLng().lng;
        console.log(singleMarker.getLatLng().lat);
    });
    //console.log(singleMarker.toGeoJSON());

    //Layer controller
    var baseMaps = {
        "OSM": osm,
        "Google street": googleStreets,
        "Google Satellite": googleSat
    };
    var overlays = {
        "Marker": singleMarker,
    };

    L.control.layers(baseMaps, overlays).addTo(map);

}