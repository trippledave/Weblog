function SiteHelpers() { }

SiteHelpers.submitReplaceForm = function() {

    var formData = $('#ReplaceForm').serialize();

    $.ajax( {
        url: '/Management/Settings/Replace',
        data: formData,
        cache: false,
        async: false,
        type: 'POST',
        success: function ( data ) {
            $( '#replacementContainer' ).html( data );
        }
    } );
};

SiteHelpers.displayJsonResult = function ( data ) {

    var panel = $( '#pnlInformation' );
    panel.removeClass( 'error' );
    panel.removeClass( 'success' );

    if ( data.success ) {
        panel.addClass( 'success' );
    } else {
        panel.addClass( 'error' );
    }

    $( "#pnlInformation" ).html( data.message );
    $( "#pnlInformation" ).fadeIn( 'slow' );
    setTimeout( function () {
        $( "#pnlInformation" ).fadeOut( 'slow' );
    }, 5000 );
}

SiteHelpers.test = function (data) {
    var rowToRemove = "#entryRow" + data;
    $(rowToRemove).remove();
}