function SiteHelpers() { }

SiteHelpers.deleteRow = function (data) {
    var rowToRemove = "#row" + data;
    $(rowToRemove).remove();
    if ($(".table tr").length == 1) {
        $(".toDeleteIfEmpty").remove();
    }
}

SiteHelpers.newPagination = function () {
    EntryPagination = new Pagination();
    EntryPagination.init();
}

SiteHelpers.commentCreated = function (id) {
    $("#displayCommentsForEntryDiv").load("./DisplayCommentsForEntry/"+id);
}

function Pagination() {

    this.init = function () {
        this.actualPage = 0;
        this.identifier = "entry";
        this.entriesPerPage = $("#pagination-container").data("entries-per-page"); // default value
        this.lastPage = $("#pagination-container").data("last-page");
        this.goToPage(EntryPagination.actualPage);
    }

    this.goToPage = function (pageNumber) {

        var entries = $("." + this.identifier);
        entries.each(function () {
            $(this).css("display", "none");
        });

        var startIndex = pageNumber * this.entriesPerPage;
        var endIndex = (pageNumber + 1) * this.entriesPerPage;
        endIndex = endIndex > entries.length ? entries.length : endIndex;

        entries.each(function (index) {
            if (index >= startIndex && index < endIndex) {
                $(this).css("display", "block");
            }
        });

        this.activatePage(pageNumber);
        this.actualPage = pageNumber;
    };

    this.previousPage = function () {
        if (!$("#pagination-previous").hasClass("disabled")) {
            this.goToPage(this.actualPage - 1);
        }
    };

    this.nextPage = function () {
        if (!$("#pagination-next").hasClass("disabled")) {
            this.goToPage(this.actualPage + 1);
        }
    };

    this.activatePage = function (pageNumber) {
        $(".pagination-element").each(function () {
            $(this).removeClass("active");
            $(this).removeClass("disabled");
        });

        $("#pagination-" + pageNumber).addClass("active");
        if (pageNumber == 0) {
            $("#pagination-previous").addClass("disabled");
        }

        if (pageNumber == this.lastPage) {
            $("#pagination-next").addClass("disabled");
        }
    };
}
var EntryPagination;

$(document).ready(function () {
    EntryPagination = new Pagination();
    EntryPagination.init();
});
