/// <reference path="hermod-ui.js" />
$(function () {

    var titleFontSize = '35px';
    var subTitleFontSize = '30px';
    var xAxisLabelFontSize = '20px';
    var yAxisLabelFontSize = '20px';
    var yAxisTextFontSize = '20px';
    var seriesDataLabelFontSize = '20px';

    var legendTextFontSize = '20px';

    //function AjaxCall(type, url, data, successHandler, errorHandler, isLoaderDisabled) {
        $.ajax({
            type: 'GET',
            url: '/Home/GetData',
            asyn: true,
            data: null,
            cache: false,
        }).done(function (data) {
            $('#getDailyStatusTotalNumberReportChart').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Sell Transactions Summary',
                    style: { fontSize: titleFontSize }
                },
                subtitle: {
                    text: '',
                    style: { fontSize: subTitleFontSize }
                },
                xAxis: {
                    categories: [
                        'Sell Transaction Count',
                        'Total Revenue',
                        'Cancel',
                        'Deliver',
                        'Return',
                        'Delete'
                    ],
                    crosshair: true,
                    labels: { style: { fontSize: xAxisLabelFontSize } }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Count',
                        style: { fontSize: yAxisTextFontSize }
                    },
                    labels: { style: { fontSize: yAxisLabelFontSize } }
                },
                legend: {
                    itemStyle: {
                        fontSize: legendTextFontSize
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:20px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    },
                    series: {
                        dataLabels: {
                            enabled: true,
                            style: { fontSize: seriesDataLabelFontSize }
                        }
                    }
                },
                series: data
            });
        }).fail(function () {
            console.log('Failed')
        });

    //AjaxCall('GET', '/Home/GetNonTransferredRecordReport', null, function (data) {
    //    $('#getNonTransferredRecordReportChart').highcharts({
    //        chart: {
    //            type: 'column'
    //        },
    //        title: {
    //            text: 'Synchronization Failed Records',
    //            style: { fontSize: titleFontSize }
    //        },
    //        subtitle: {
    //            text: ''
    //        },
    //        xAxis: {
    //            categories: [
    //              'Create',
    //              'Accept',
    //              'Cancel',
    //              'Deliver',
    //              'Return',
    //              'Delete'
    //            ],
    //            type: 'status',
    //            labels: { style: { fontSize: xAxisLabelFontSize } }

    //        },
    //        yAxis: {
    //            title: {
    //                text: 'Total Number of Records',
    //                style: { fontSize: yAxisTextFontSize }
    //            },
    //            labels: { style: { fontSize: yAxisLabelFontSize } }
    //        },
    //        legend: {
    //            enabled: false
    //        },
    //        plotOptions: {
    //            series: {
    //                borderWidth: 0,
    //                dataLabels: {
    //                    enabled: true,
    //                    format: '{point.y}',
    //                    style: { fontSize: seriesDataLabelFontSize }
    //                }
    //            }
    //        },

    //        tooltip: {
    //            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
    //            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> of total<br/>'
    //        },
    //        series: [{
    //            name: 'Status',
    //            data: data
    //        }]
    //    });
    //}, commonErrorHandler);
    //AjaxCall('GET', '/Home/GetTotalCountByShipmentMethodReport', null, function (data) {

    //    $('#getTotalCountByShipmentMethodReportChart').highcharts({
    //        chart: {
    //            type: 'column'
    //        },
    //        title: {
    //            text: 'Undelivered Deliveries By Status',
    //            style: { fontSize: titleFontSize }
    //        },
    //        xAxis: {
    //            categories: [
    //                'Create',
    //                'Accept',
    //                'Cancel'
    //            ],
    //            crosshair: true,
    //            labels: { style: { fontSize: xAxisLabelFontSize } }
    //        },
    //        yAxis: {
    //            min: 0,
    //            title: {
    //                text: 'Total Count',
    //                style: { fontSize: yAxisTextFontSize }
    //            },
    //            labels: { style: { fontSize: yAxisLabelFontSize } }
    //        },
    //        legend: {
    //            itemStyle: {
    //                fontSize: legendTextFontSize
    //            }
    //        },
    //        tooltip: {
    //            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
    //            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
    //                '<td style="padding:0"><b>{point.y}</b></td></tr>',
    //            footerFormat: '</table>',
    //            shared: true,
    //            useHTML: true
    //        },
    //        plotOptions: {
    //            column: {
    //                pointPadding: 0.2,
    //                borderWidth: 0
    //            },
    //            series: {
    //                dataLabels: {
    //                    enabled: true,
    //                    style: { fontSize: seriesDataLabelFontSize }
    //                }
    //            }
    //        },
    //        series: data
    //    });
    //}, commonErrorHandler);

    //AjaxCall('GET', '/Home/GetIrregularEntiriesByShipmentMethodReport', null, function (data) {
    //    $('#getIrregularEntiriesByShipmentMethodReportChart').highcharts({
    //        chart: {
    //            plotBackgroundColor: null,
    //            plotBorderWidth: null,
    //            plotShadow: false,
    //            type: 'pie'
    //        },
    //        title: {
    //            text: 'End Of Day Summary',
    //            style: { fontSize: titleFontSize }
    //        },
    //        tooltip: {
    //            pointFormat: '<b>{point.y} - </b>  {point.percentage:.1f} %'
    //        },
    //        legend: {
    //            itemStyle: {
    //                fontSize: legendTextFontSize
    //            }
    //        },
    //        plotOptions: {
    //            pie: {
    //                allowPointSelect: true,
    //                cursor: 'pointer',
    //                dataLabels: {
    //                    enabled: true,
    //                    format: '<b>{point.name}</b>: {point.y} - {point.percentage:.1f} %',
    //                    style: {
    //                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black',
    //                        fontSize: seriesDataLabelFontSize
    //                    }
    //                },
    //                showInLegend: true
    //            }
    //        },
    //        series:
    //            [{
    //                data: data
    //            }]
    //    });
    //}, commonErrorHandler);
    //AjaxCall('GET', '/Home/GetNonDeliveredDeliveriesSummary', null, function (data) {

    //    var prepareHtml = function (value) {
    //        return '<span style="font-size:30px">' + value + '</span>';
    //    }

    //    if (data && data.length >= 2) {
    //        $('#nonCreatedDeliveriesSummary_status').html(prepareHtml(data[0].status));
    //        $('#nonCreatedDeliveriesSummary_count').html(prepareHtml(data[0].count));
    //        $('#nonCreatedDeliveriesSummary_lastSinceDay').html(prepareHtml(data[0].lastSinceDay));


    //        $('#nonAcceptedDeliveriesSummary_status').html(prepareHtml(data[1].status));
    //        $('#nonAcceptedDeliveriesSummary_count').html(prepareHtml(data[1].count));
    //        $('#nonAcceptedDeliveriesSummary_lastSinceDay').html(prepareHtml(data[1].lastSinceDay));
    //    } else {
    //        commonErrorHandler(data, '500', 'Array cannot be null or its length can\'be smaller that 2');
    //    }



    //}, commonErrorHandler);

    //AjaxCall('GET', '/Home/GetWeeklyDeliveryStatusReport', null, function (result) {
    //    debugger;
    //    $('#getWeeklyDeliveryStatusReportChart').highcharts({
    //        chart: {
    //            type: 'line'
    //        },
    //        title: {
    //            text: 'WEEKLY DELIVERY STATUS', style: { fontSize: titleFontSize }
    //        },
    //        xAxis: {
    //            categories: result.DayNameList,
    //            labels: { style: { fontSize: xAxisLabelFontSize } }
    //        },
    //        yAxis: {
    //            title: {
    //                text: 'Count',
    //                style: { fontSize: yAxisTextFontSize }
    //            },
    //            labels: { style: { fontSize: yAxisLabelFontSize } }
    //        },
    //        plotOptions: {
    //            line: {
    //                dataLabels: {
    //                    enabled: true,
    //                    style: { fontSize: 14 }
    //                }
    //            }
    //        },
    //        series: result.Items
    //    });
    //}, commonErrorHandler);


    //AjaxCall('GET', '/Home/GetWeeklyIrregularEntiriesReport', null, function (result) {
    //    debugger;
    //    $('#getWeeklyIrregularEntiriesReportChart').highcharts({
    //        chart: {
    //            type: 'area'
    //        },
    //        title: {
    //            text: 'Weekly End Of Day Summary',
    //            style: { fontSize: titleFontSize }
    //        },

    //        xAxis: {
    //            categories: result.DayNameList,
    //            tickmarkPlacement: 'on',
    //            title: {
    //                enabled: false
    //            },
    //            labels: { style: { fontSize: xAxisLabelFontSize } }

    //        },
    //        yAxis: {
    //            title: {
    //                text: 'Count',
    //                style: { fontSize: yAxisTextFontSize }
    //            },
    //            labels: { style: { fontSize: yAxisLabelFontSize } }

    //        },
    //        tooltip: {
    //            enabled: false
    //        },
    //        legend: {
    //            enabled: false
    //        },
    //        plotOptions: {
    //            area: {
    //                stacking: 'normal',
    //                lineColor: 'red',
    //                lineWidth: 2,
    //                dataLabels: {
    //                    enabled: true,
    //                    format: '{point.y}',
    //                    color: 'black',
    //                    style: { fontSize: seriesDataLabelFontSize }
    //                },
    //                marker: {
    //                    lineWidth: 10,
    //                    lineColor: 'red'
    //                }
    //            }
    //        },
    //        series: [{
    //            name: result.Item.Name,
    //            data: result.Item.Data,
    //            color: 'orange'
    //        }]
    //    });
    //}, commonErrorHandler);


});