﻿"use strict";

function functionProfile() {

    //var dropdowns = document.getElementById('users-menu');

    //if ((dropdowns.getAttribute('class')).localeCompare('user-menu dropdown-menu show')===0) {

    //    for (i = 0; i < dropdowns.length; i++) {
    //        var openDropdown = dropdowns[i];
    //        if (openDropdown.classList.contains('show')) {
    //            openDropdown.classList.remove('show');
    //        }
    //    }
    //}

    document.getElementById("users-menu").classList.toggle("show");
     if (!event.target.matches('users-menu')) {
        var dropdowns1 = document.getElementsByClassName("dropdown-menu show");
        var i;
        for (i = 0; i < dropdowns1.length; i++) {
            var openDropdown1 = dropdowns1[i];
            if (openDropdown1.classList.contains('show')) {
                openDropdown1.classList.remove('show');
            }
        }
    }

}

function functionFlag() {
    if (!event.target.matches('flag')) {
        var dropdowns = document.getElementsByClassName("user-menu dropdown-menu show");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }

    document.getElementById("flag").classList.toggle("show");

}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('users-menu') || !event.target.matches('flag')) {

        var dropdowns = document.getElementsByClassName("user-menu dropdown-menu show");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }

        var dropdowns1 = document.getElementsByClassName("dropdown-menu show");
        
        for (i = 0; i < dropdowns1.length; i++) {
            var openDropdown1 = dropdowns1[i];
            if (openDropdown1.classList.contains('show')) {
                openDropdown1.classList.remove('show');
            }
        }
    }
}

   


