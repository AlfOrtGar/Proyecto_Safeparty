﻿
function initMap() {

    // 1. Inicia el mapa 
    const Madrid = { lat: 40.34, lng: -3.74 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: Madrid,
    });


    listaDiscotecas.forEach(discoteca => {

        // 2.1 Creamos la tarjeta

        const contentString =
            '<div id="content">' +
            '<div id="siteNotice">' +
            "</div>" +
            '<h1 id="firstHeading" class="firstHeading"> ' + discoteca.name + '</h1>' +
            '<div id="bodyContent">' +
            "<p>" + discoteca.address + "</p>" +
            "<h3> Valoración: " + discoteca.valoration + "</h3>" +
            "<h4> Teléfono: " + discoteca.phone + "</h4>" +
            "<h4> ID:" + discoteca.id + "</h4>" +
            "<a href=https://safeparty.com/" + discoteca.id + "> Ver más en Safeparty </a>"
        "</div>" +
            "</div>";

        const infowindow = new google.maps.InfoWindow({
            content: contentString,
            ariaLabel: "Tarjeta",
        });

        // 2.2 Creamos el marcador

        nuevaPosicion = {
            lat: discoteca.lat,
            lng: discoteca.lng,
        }

        const marker = new google.maps.Marker();

        if (discoteca.valoration >= 4) {
            console.log("Discoteca buena.");
            const marker = new google.maps.Marker({
                position: nuevaPosicion,
                map,
                title: "Discoteca buena",
                icon: "../lib/pictures/checked.png",
            });
            marker.addListener("click", () => {
                infowindow.open({
                    anchor: marker,
                    map,
                });
            });
        }

        if ((discoteca.valoration >= 2.5) && (discoteca.valoration < 4)) {
            console.log("Discoteca regulera.");
            const marker = new google.maps.Marker({
                position: nuevaPosicion,
                map,
                title: "Discoteca regulera",
                icon: "../lib/pictures/warning.png",
            });
            marker.addListener("click", () => {
                infowindow.open({
                    anchor: marker,
                    map,
                });
            });
        }

        if (discoteca.valoration < 2.5) {
            console.log("Discoteca mala.");
            const marker = new google.maps.Marker({
                position: nuevaPosicion,
                map,
                title: "Discoteca mala",
                icon: "../lib/pictures/cancel.png",
            });
            marker.addListener("click", () => {
                infowindow.open({
                    anchor: marker,
                    map,
                });
            });
        }

    })
}


window.initMap = initMap;