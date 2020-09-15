
var charts = [];

$(function () {
    $.post(charts_ajax_url, function (response) {
        console.log("chart data is "+JSON.stringify(response));
        LoadCharts(response);
    })

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        charts.forEach(function (chart) {
            chart.reflow();
        })
    });
})




function LoadCharts(data) {

    charts = [];

    //==================================================

    charts.push(Highcharts.chart('allMobileCarRequest', {
        chart: {
            type: 'line'
        },
        title: {
            text: AllMobileCarRequest
        },
        credits: {
            enabled: false
        },
        xAxis: [{
            categories: data.Requisition.categories,
            reversed: false,
            labels: {
                step: 1
            }
        }, {
            opposite: true,
            reversed: false,
            categories: data.Requisition.categories,
            linkedTo: 0,
            labels: {
                step: 1
            }
        }],
        yAxis: {
            title: {
                text: null
            },
            labels: {
                formatter: function () {
                    return Math.abs(this.value);
                }
            }
        },
        plotOptions: {
            bar: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    formatter: function () {
	                    return Math.abs(this.point.y);
	                },
	                inside: false
                }
            },
            line: {
                dataLabels: {
                    enabled: true
                }
            }
        },
        tooltip: {
            enabled: false
        },
        series: data.Requisition.series
    }));

    //=====================================================
}


