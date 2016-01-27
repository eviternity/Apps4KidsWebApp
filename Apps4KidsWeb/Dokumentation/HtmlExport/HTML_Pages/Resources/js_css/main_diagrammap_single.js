var minScaleFactor = 40;

function updateZoom() {
	var zoom = $('#slider').slider('value');
	if (isNaN(zoom) == false) $('#zoomTextId').text(zoom + '%');
	var scale = $('#slider').slider('value') / 100;
	$('#diagramId').css('zoom', (zoom) + '%');
	$('#diagramId').css('-moz-transform', 'scale(' + (scale) + ')');
}

function getOptimalImageScaleFactor() {
	var minScaleFactor = 50;
	var windowWidth = $(window).width();
	var imageWidth = parseInt($('#diagramId').css('width'));
	var scaleFactor = ((windowWidth - 30) / imageWidth) * 100;
	return scaleFactor;
}

$('#slider').slider(
{
	min: minScaleFactor,
	max: 100,
	width: 50,
	value: getOptimalImageScaleFactor(),
	slide: updateZoom,
	change: updateZoom
});


var treshholderOrig = 205;
var treshholder = treshholderOrig;
function resizeDiagramImage() {
	$('#diagramContainerId').css('height', $(window).height() - treshholder);
}
$(window).bind('resize', resizeDiagramImage);
resizeDiagramImage();

if (getItem('previousPosition') == undefined || getItem('previousPosition') == null) { setItem('previousPosition', '-1'); }
if (getItem('previousPosition') != '-1') {
	$('#diagramContainerId').css('borderSpacing', treshholder);
	$('#toggleDetailsId').css('bottom', '5px');
	$('#diagramContainerId').animate({ borderSpacing: 34 },
	{
		step: function (now, fx) {
			treshholder = now;
			resizeDiagramImage();
		},
		duration: 0
	});
	$('#toggleDetailsId').find('img').animate({ borderSpacing: 90 },
	{
		step: function (now, fx) {
			$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
			$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
			$(this).css('-ms-transform', 'rotate(' + now + 'deg)');
			$(this).css('-o-transform', 'rotate(' + now + 'deg)');
			$(this).css('transform', 'rotate(' + now + 'deg)');
		},
		duration: 0
	});
} else {
	$('#toggleDetailsId').animate({ bottom: parseInt($('#notesPanelId').height()) - 19 }, 0);
}

$('#toggleDetailsId').click(function () {
	if (getItem('previousPosition') == '-1') {
		setItem('previousPosition', $('#toggleDetailsId').css('bottom'));
		$('#diagramContainerId').css('borderSpacing', treshholder);

		$('#toggleDetailsId').animate({ bottom: '5px' }, 580);
		$('#diagramContainerId').animate({ borderSpacing: 34 },
		{
			step: function (now, fx) {
				treshholder = now;
				resizeDiagramImage();
			},
			duration: 500
		});

		$('#toggleDetailsId').find('img').animate({ borderSpacing: 90 },
		{
			step: function (now, fx) {
				$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
				$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
				$(this).css('-ms-transform', 'rotate(' + now + 'deg)');
				$(this).css('-o-transform', 'rotate(' + now + 'deg)');
				$(this).css('transform', 'rotate(' + now + 'deg)');
			},
			duration: 1000
		});
	}
	else {
		$('#toggleDetailsId').animate({ bottom: getItem('previousPosition') }, 440);
		$('#diagramContainerId').animate({ borderSpacing: treshholderOrig },
		{
			step: function (now, fx) {
				treshholder = now;
				resizeDiagramImage();
			},
			duration: 500
		});

		setItem('previousPosition', '-1');

		$('#toggleDetailsId').find('img').animate({ borderSpacing: 270 },
		{
			step: function (now, fx) {
				$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
				$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
				$(this).css('-ms-transform', 'rotate(' + now + 'deg)');
				$(this).css('-o-transform', 'rotate(' + now + 'deg)');
				$(this).css('transform', 'rotate(' + now + 'deg)');
			},
			duration: 1000
		});
	}
});

$(document).ready(function() {
	$('ul > br').remove();
});