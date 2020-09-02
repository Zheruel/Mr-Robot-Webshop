(function ($) {
  "use strict"; // Start of use strict

  var categoriesController = $.getScript(
    "js/categoriesController.js",
    function (script, textStatus, jqXHR) {}
  );

  categoriesController.done(function () {
    drawHome();
  });

  //Sorting by category
  $(document).on("click", "[navItemType = 'category']", function () {
    var categoryId = this.id;

    sortByCategory(categoryId);
  });

  $(document).on("click", "[navItemType = 'subCategory']", function () {
    var subCategoryId = this.id;

    sortBySubCategory(subCategoryId);
  });

  $(document).on("click", "#resetFilter", function () {
    drawHome();
  });

})(jQuery); // End of use strict
