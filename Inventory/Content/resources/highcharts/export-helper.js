/**
 * Create a global getSVG method that takes an array of charts as an
 * argument
 */
Highcharts.getSVG = function (charts) {
    var svgArr = [],
        top = 0,
        width = 0;

    Highcharts.each(charts, function (chart) {
        var svg = chart.getSVG(),
            // Get width/height of SVG for export
            svgWidth = +svg.match(
                /^<svg[^>]*width\s*=\s*\"?(\d+)\"?[^>]*>/
            )[1],
            svgHeight = +svg.match(
                /^<svg[^>]*height\s*=\s*\"?(\d+)\"?[^>]*>/
            )[1];

        svg = svg.replace(
            '<svg',
            '<g transform="translate(0,' + top + ')" '
        );
        svg = svg.replace('</svg>', '</g>');

        top += svgHeight;
        width = Math.max(width, svgWidth);

        svgArr.push(svg);
    });

    return '<svg height="' + top + '" width="' + width +
        '" version="1.1" xmlns="http://www.w3.org/2000/svg">' +
        svgArr.join('') + '</svg>';
};

/**
 * Create a global exportCharts method that takes an array of charts as an
 * argument, and exporting options as the second argument
 */
Highcharts.exportCharts = function (charts, options) {

    // Merge the options
    options = Highcharts.merge(Highcharts.getOptions().exporting, options);

    // Post to export server
    Highcharts.post(options.url, {
        filename: options.filename || 'chart',
        type: options.type,
        width: options.width,
        svg: Highcharts.getSVG(charts)
    });
};