

var location_var;

var eventData0 = [];

//------------------------------------------------------------------------------------------------------------------------------
let calA = new Calendar({
  
  id: "#calendar-a",

  theme: "glass",



  weekdayType: "long-upper",
  monthDisplayType: "long",
  calendarSize: "large",
  layoutModifiers: ["month-left-align"],

    eventsData: eventData0,

    dateChanged: (currentDate, eventsData) => {
        console.log("date change", currentDate, eventsData);
        currentDate_var = currentDate;

        console.log(currentDate_var);



      var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
  
      for (var i = 0; i < elements.length; i++) {
         
          elements[i].style.pointerEvents = "none";
          elements[i].style.backgroundColor = "#584475";
    
    }
      
    },
    monthChanged: (currentDate, events) => {
      console.log("month change", currentDate, events);
      var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
  
      for (var i = 0; i < elements.length; i++) {
         
          elements[i].style.pointerEvents = "none";
          elements[i].style.backgroundColor = "#584475";
    
    }
  }

  });

  //------------------------------------------------------------------------------------------------------------------------------
  /*
  
  let calB = new Calendar({
  
    id: "#calendar-b",
    theme: "glass",
    // border: "5px solid black",
    weekdayType: "long-upper",
    monthDisplayType: "long",
    // headerColor: "yellow",
    // headerBackgroundColor: "black",
    calendarSize: "large",
    layoutModifiers: ["month-left-align"],

      eventsData: eventData1,

  
  
      dateChanged: (currentDate, eventsData) => {
        console.log("date change", currentDate, eventsData);
        currentDate_var = currentDate;
  
        var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
    
        for (var i = 0; i < elements.length; i++) {
           
            elements[i].style.pointerEvents = "none";
            elements[i].style.backgroundColor = "lightblue";
      
      }
        
      },
      monthChanged: (currentDate, events) => {
        console.log("month change", currentDate, events);
        var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
    
        for (var i = 0; i < elements.length; i++) {
           
            elements[i].style.pointerEvents = "none";
            elements[i].style.backgroundColor = "lightblue";
      
      }
      }
  
    });

   let calC = new Calendar({
  
      id: "#calendar-c",
      theme: "glass",
    
      weekdayType: "long-upper",
      monthDisplayType: "long",
      calendarSize: "large",
      layoutModifiers: ["month-left-align"],
    

       eventsData: eventData2,

    
    
        dateChanged: (currentDate, eventsData) => {
          console.log("date change", currentDate, eventsData);
          currentDate_var = currentDate;
    
          var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
      
          for (var i = 0; i < elements.length; i++) {
             
              elements[i].style.pointerEvents = "none";
              elements[i].style.backgroundColor = "lightblue";
        
        }
          
        },
        monthChanged: (currentDate, events) => {
          console.log("month change", currentDate, events);
          var elements = document.getElementsByClassName("calendar__day calendar__day-active calendar__day-event");
      
          for (var i = 0; i < elements.length; i++) {
             
              elements[i].style.pointerEvents = "none";
              elements[i].style.backgroundColor = "lightblue";
        
        }
        }
    
   });
     
   */

//------------------------------------------------------------------------------------------------------------------------------



//-----------------------------------------------------------------------------------------------------------





/*
function getEventData_1() {

    eventData0 = [];

    $.ajax({
        url: "/Appointment/Appointments_2",
        type: "GET",
        dataType: "json",
        success: function (appointments_2) {
            for (var i = 0; i < appointments_2.length; i++) {
                var appointment = appointments_2[i];
                var eventData = {
                    customer: appointment.customer,
                    start: appointment.start,
                    end: appointment.end,
                    time: appointment.time
                };
                eventData1.push(eventData);

            }
            console.log(eventData1);
            calB.setEventsData(eventData1);
        },
        error: function () {
            alert("Fail");
        }
    });
}


function getEventData_2() {

    eventData2 = [];

    $.ajax({
        url: "/Appointment/Appointments_3",
        type: "GET",
        dataType: "json",
        success: function (appointments_3) {
            for (var i = 0; i < appointments_3.length; i++) {
                var appointment = appointments_3[i];
                var eventData = {
                    customer: appointment.customer,
                    start: appointment.start,
                    end: appointment.end,
                    time: appointment.time
                };
                eventData2.push(eventData);

            }
            console.log(eventData2);
            calC.setEventsData(eventData2);
        },
        error: function () {
            alert("Fail");
        }
    });
}
*/

/*)
function cancel_appointment() {

    if (window.confirm("Are you sure you want to cancel?")) {
        var current_time = new Date().getTime();
        var time_interval = 24 * 60 * 60 * 1000;
        var i = eventData.findIndex(element => element.customer === document.getElementById("username").value);
        username = document.getElementById("username").value;
        var given_time = Date.parse(eventData[i].time);
        console.log(current_time + "current time")
        console.log(eventData[i]);
        console.log(given_time + "given time");

        if (current_time - given_time < time_interval){
            $.ajax({
                type: 'POST',
                url: '/Appointment/cancelAppointment?username=' + username,
                success: function () {
                    eventData.splice(i, 1);
                    console.log("removed Appointment," + eventData);
                },
                error: function (response) {
                    alert('An error occurred while canceling the appointment.');
                }
            });
        }

            }
        
    }

    */



/*

  function      table_maker(){

            area = document.getElementById("governate").value;

            if (area === "hawally") {
                eventData = eventData0;
            } else if (area === "ahmadi") {
                eventData = eventData1;
            } else {
                eventData = eventData2;
            }



        }

        */
