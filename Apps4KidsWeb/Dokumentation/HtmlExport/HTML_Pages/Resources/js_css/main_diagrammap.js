if (getItem('navWidth') == undefined || getItem('navWidth') == null) {
	setItem('navWidth', '430px');
}
$('#navigationHeaderId').css('width', getItem('navWidth'));

var toggleNavigationOffset = 22;

function onResizeWindow() {
	$('#contentId').css('height', $(window).height() - 48 + 'px');
	$('#toggleNavigationId').css('left', parseInt($('#navigationContainerId').css('width')) - toggleNavigationOffset + 'px');
}

var onTableResized = function (e) {
	$('#iframeMaskId').hide();
	_width = $('div[class="JCLRgrip"]').css('left');
	_widthAdjusted = parseInt(_width) - 8 + 'px';
};

var OnTableResizing = function (e) {
	setItem('navWidth', $('#navigationHeaderId').css('width'));
	$('#toggleNavigationId').css('left', parseInt($('#navigationContainerId').css('width')) - toggleNavigationOffset + 'px');
};

var onTableBeforeResized = function (e) {
	$('#iframeMaskId').show();
};

$(document).ready(function () {
	$('#navigationTreeId').jstree({
			'xml_data':
			{
				'data': navigationData,
				'xsl': 'nest'
			},
			'themes': {
				'theme': 'apple',
				'dots': false,
				'icons': false
			},
			'types':
			{
				'valid_children': ['folder'],
				'types':
				{
					'placeholder': { 'icon': { 'image': 'HTML_Pages/Resources/images/placeholder.png' } },
					'Activity': { 'icon': { 'image': 'HTML_Pages/Resources/images/ActivityDiagram.png' } },
					'Component': { 'icon': { 'image': 'HTML_Pages/Resources/images/ComponentDiagram.png' } },
					'Custom': { 'icon': { 'image': 'HTML_Pages/Resources/images/CustomDiagram.png' } },
					'Composite': { 'icon': { 'image': 'HTML_Pages/Resources/images/CompositeDiagram.png' } },
					'Deployment': { 'icon': { 'image': 'HTML_Pages/Resources/images/DeploymentDiagram.png' } },
					'Logical': { 'icon': { 'image': 'HTML_Pages/Resources/images/LogicalDiagram.png' } },
					'Object': { 'icon': { 'image': 'HTML_Pages/Resources/images/ObjectDiagram.png' } },
					'Package': { 'icon': { 'image': 'HTML_Pages/Resources/images/PackageDiagram.png' } },
					'Sequence': { 'icon': { 'image': 'HTML_Pages/Resources/images/SequenceDiagram.png' } },
					'Statechart': { 'icon': { 'image': 'HTML_Pages/Resources/images/Statechart.png' } },
					'UseCase': { 'icon': { 'image': 'HTML_Pages/Resources/images/UseCaseDiagram.png' } }
				}
			},
			'plugins': ['themes', 'xml_data', 'ui', 'crrm', 'hotkeys', 'search', 'adv_search', 'types']
		})
		.bind('select_node.jstree', function(event, data) {
			try {
				document.location.href = data.rslt.obj.attr('href');
			} catch (x) {;
			}
		})
		.one('loaded.jstree', function(event, data) {
			var file = urlParams['file'];
			var url = document.location.href;
			url = url.substring(0, url.lastIndexOf('/')) + '/';
			if (file != undefined) {
				$('#diagramNameId').text(file.substring(0, file.length - 42).split('_').join(' '));
				document.getElementById('contentFrameID').src = url + 'HTML_Pages/' + file;
			} else {
				file = $('#navigationTreeId ul > li:first').attr('href').replace('HTML_Pages/', '');
				$('#diagramNameId').text(file.substring(0, file.length - 42).split('_').join(' '));
				document.getElementById('contentFrameID').src = url + $('#navigationTreeId ul > li:first').attr('href');
			}
			$('li[href="HTML_Pages/' + file + '"] a').first().addClass('jstree-clicked');


			if (getItem('navScrollTop') == undefined || getItem('navScrollTop') == null) {
				setItem('navScrollTop', '0');
			}
			$('#navigationContainerId').scrollTop(parseInt(getItem('navScrollTop')));
			window.onbeforeunload = function() { setItem('navScrollTop', $('#navigationContainerId').scrollTop() + ''); }


			$('#contentTable').colResizable(
			{
				liveDrag: true,
				onBeforeDrag: onTableBeforeResized,
				onResize: onTableResized,
				onDrag: OnTableResizing
			});

			$('#navigationTreeId').find('ul > li ').each(function(key, value) {
				$(value).find('a').qtip(
				{
					content: $('#tooltip_' + $(value).attr('id')).html(),
					show: { delay: 600 },
					position:
					{
						target: 'mouse',
						adjust: { mouse: true },
						viewport: $(window)
					}
				});
			});
		});


	$('#navigationSearchId').keyup(function() {
		$('#navigationTreeId').jstree('search', $('#navigationSearchId').val());
	});

	$(window).bind('resize', onResizeWindow);
	onResizeWindow();

	if (getItem('previousWidth') == undefined || getItem('previousWidth') == null) {
		setItem('previousWidth', '-1');
	}
	if (getItem('previousWidth') != '-1') {
		$('#toggleNavigationId').css('left', '-40px');
		$('#navigationHeaderId').css('width', '0px');
		$('div[class="JCLRgrip"]').css('left', '0px');
		$('#toggleNavigationId').find('img').animate({ borderSpacing: 180 },
		{
			step: function(now, fx) {
				$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
				$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
				$(this).css('-ms-transform', 'rotate(' + now + 'deg)');
				$(this).css('-o-transform', 'rotate(' + now + 'deg)');
				$(this).css('transform', 'rotate(' + now + 'deg)');
			},
			duration: 0
		});
	} else {
		$('#toggleNavigationId').css('left', parseInt($('#navigationContainerId').css('width')) - 20);
		$('#toggleNavigationId').animate({ left: parseInt($('#navigationContainerId').css('width')) - toggleNavigationOffset + 'px' }, 0);
	}

	$('#toggleNavigationId').click(function() {
		if (getItem('previousWidth') == '-1') {
			setItem('previousWidth', $('#navigationContainerId').css('width'));

			$('#toggleNavigationId').animate({ left: '-40px' }, 690);
			$('#navigationHeaderId').animate({ width: '0px' }, 600);
			$('div[class="JCLRgrip"]').animate({ left: '0px' }, 600);

			$('#toggleNavigationId').find('img').animate({ borderSpacing: 180 },
			{
				step: function(now, fx) {
					$(this).css('-webkit-transform', 'rotate(' + now + 'deg)');
					$(this).css('-moz-transform', 'rotate(' + now + 'deg)');
					$(this).css('-ms-transform', 'rotate(' + now + 'deg)');
					$(this).css('-o-transform', 'rotate(' + now + 'deg)');
					$(this).css('transform', 'rotate(' + now + 'deg)');
				},
				duration: 1000
			});
		} else {
			$('#toggleNavigationId').animate({ left: (parseInt(getItem('previousWidth')) - toggleNavigationOffset) + 'px' }, 530);
			$('#navigationHeaderId').animate({ width: (parseInt(getItem('previousWidth')) + 5) + 'px' }, 600);
			$('div[class="JCLRgrip"]').animate({ left: (parseInt(getItem('previousWidth')) + 7) + 'px' }, 600);
			setItem('previousWidth', '-1');

			$('#toggleNavigationId').find('img').animate({ borderSpacing: 0 },
			{
				step: function(now, fx) {
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
});