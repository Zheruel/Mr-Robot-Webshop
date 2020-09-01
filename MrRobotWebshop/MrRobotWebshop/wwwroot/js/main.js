(function($) {
  "use strict"; // Start of use strict

  var categoriesController = $.getScript("js/categoriesController.js", function (script, textStatus, jqXHR) {});

  categoriesController.done(function() {
    drawHome();
  });

})(jQuery); // End of use strict
