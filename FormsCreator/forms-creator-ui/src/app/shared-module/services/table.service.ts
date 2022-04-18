import {Injectable} from "@angular/core";
// @ts-ignore
import * as $ from "jquery";

declare var jQuery: any;

@Injectable()
export class TableService {

  public setUpTopScroll(): void{
    let tableWrapperClass = ".table-wrapper";
    let tableTopScrollId = "#table-top-scroll-";
    let tableTopScrollInnerClass = ".inner";
    let topBarId = "#top-bar";
    let tableTopScrollHtml = "<div id=\"table-top-scroll-id\" class=\"table-top-scroll\"><div class=\"inner\"></div></div>";
    let responsiveWidth = 768;
    let topBarHeight = $(topBarId).outerHeight();
    let isHeaderFloated = false;

    let tableWrappers = $(tableWrapperClass);
    if (tableWrappers.length === 0){
      return;
    }

    // add or remove top scroll on page scrolling
    $(window).scroll(function() {
      tableWrappers = $(tableWrapperClass);
      if (tableWrappers.length === 0){
        return;
      }

      $(tableWrapperClass).each(function(i:any) {
        // @ts-ignore
        var tableWrapper = $(this);

        let tableHeadHeight = tableWrapper.find("table thead").outerHeight();
        let headerHeight = $("header").outerHeight();
        let wrapperToTopLength = tableWrapper.offset().top - $(window).scrollTop();
        let top = topBarHeight;
        if ($(window).width() <= responsiveWidth){
          top = headerHeight;
        }

        // check if not hidden and if it's under top bar
        let shouldShow = tableWrapper.is(":visible") && wrapperToTopLength - top <= 0;
        let tableTopScroll = $(tableTopScrollId + i);

        // when table wrapper is visible
        if(!shouldShow){
          if (tableTopScroll.length && tableTopScroll.is(":visible")){
            tableTopScroll.hide();
          }
        }
        // when table wrapper is not visible
        else {
          if (tableTopScroll.length) {
            if (!tableTopScroll.is(":visible")) {
              tableTopScroll.show();

              // set proper top position for fixed
              tableTopScroll.css({top: topBarHeight + tableHeadHeight - 1 + "px"});

              // set top scroll container width to preserve scroll when position fixed
              tableTopScroll.width(tableWrapper.outerWidth());

              // set the width of the scroll itself
              tableTopScroll.find(tableTopScrollInnerClass).width(tableWrapper.find("table").outerWidth());
            }
          }
          else {
            // insert table top scroll
            $(tableTopScrollHtml.replace("table-top-scroll-id", "table-top-scroll-" + i)).insertBefore(tableWrapper);
            tableTopScroll = $(tableTopScrollId + i);

            // set proper top position for fixed
            tableTopScroll.css({top: topBarHeight + tableHeadHeight - 1 + "px"});

            // set top scroll container width to preserve scroll when position fixed
            tableTopScroll.width(tableWrapper.outerWidth());

            // set the width of the scroll itself
            tableTopScroll.find(tableTopScrollInnerClass).width(tableWrapper.find("table").outerWidth());

            // set double scroll
            tableTopScroll.scroll(function () {
              tableWrapper
                .scrollLeft(tableTopScroll.scrollLeft());
            });
            tableWrapper.scroll(function () {
              tableTopScroll
                .scrollLeft(tableWrapper.scrollLeft());
            });
          }
        }

        if (shouldShow) {
          let tw = jQuery(tableWrapper);
          let table = tw.find("table");
          if (!isHeaderFloated) {
            table.on("floatThead", function(e:any, isFloated:any, floatContainer:any){
              isHeaderFloated = isFloated;
            });

            table.floatThead({
              position: "fixed",
              top: top,
              responsiveContainer: function ($table:any) {
                return $table.closest(tableWrapperClass);
              }
            });
          }
        } else {
          let tw = jQuery(tableWrapper);
          tw.find("table").floatThead("destroy");
        }
      });

    });
  }
}
