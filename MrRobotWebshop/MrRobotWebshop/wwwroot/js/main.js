(function ($) {
  "use strict"; // Start of use strict

  var categoriesController = $.getScript(
    "js/categoriesController.js",
    function (script, textStatus, jqXHR) {}
  );

  categoriesController.done(function () {
    drawHome();

    $("#crumbs").text("Home");
  });

  //Sorting by category
  $(document).on("click", "[navItemType = 'category']", function () {
    var categoryId = this.id;

    var children = $(this).children();
    children = children[0].text.split(" ");

    sortByCategory(categoryId);

    $("#crumbs").text("Home/" + children[0]);
  });


  //Sorting by subcategory
  $(document).on("click", "[navItemType = 'subCategory']", function () {
    var subCategoryId = this.id;

    var children = $(this).children();
    children = children[0].text.split(" ");

    var oldText = $("#crumbs").text();

    sortBySubCategory(subCategoryId);
    $("#crumbs").text(oldText + "/" + children[0]);
  });

//Reset to home
  $(document).on("click", "#resetFilter", function () {
    drawHome();

    $("#crumbs").text("Home");
  });

})(jQuery); // End of use strict
