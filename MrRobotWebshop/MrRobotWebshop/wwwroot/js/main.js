(function($) {
  "use strict"; // Start of use strict
  
  $("#manageCategories").click(function (e) { 
    e.preventDefault();
    $.getScript("js/categoriesController.js", function (script, textStatus, jqXHR) {
    });
  });

})(jQuery); // End of use strict
