$.fn.paddingTop = function () {
	// clone the interesting element
	// append it to body so that CSS can take effect
	var clonedElement = this.
		 clone().
		 appendTo(document.body);

	// get its height
	var innerHeight = clonedElement.
		 innerHeight();

	// set its padding top to 0
	clonedElement.css("padding-top", "0");

	// get its new height
	var innerHeightWithoutTopPadding = clonedElement.
		 innerHeight();

	// remove the clone from the body
	clonedElement.remove();

	// return the difference between the height with its padding and without its padding
	return innerHeight - innerHeightWithoutTopPadding;

};

function checkSearch(searchString, supressTakeScroll) {
	var currentElement = $(window.location.hash)[0];

	if (searchString != '') {
		var re = new RegExp(searchString.toLowerCase(), 'g');
		$('.section :header.header, .navitem a').each(function () {
			if ($(this).html().toLowerCase().match(re)) {
				$(this).parent().css('display', 'block');
			} else {
				$(this).parent().css('display', 'none');
			}
		});
		if (currentElement != null && currentElement != undefined) {
			if (!supressTakeScroll) {
				setTimeout(function () {
					$(window).scrollTop($(currentElement).offset().top - $('#mainContent').paddingTop());
				}, 10);
			}
		}
	} else {
		$('.section, .navitem').css('display', 'block');
		if (currentElement != null && currentElement != undefined) {
			if (!supressTakeScroll) {
				setTimeout(function () {
					$(window).scrollTop($(currentElement).offset().top - $('#mainContent').paddingTop());
				}, 10);
			}
		}
	}
}

var currentDiagram;
var showDiagram = function (container) {
	if (currentDiagram == undefined) {
		currentDiagram = container;
		var imgSize = $(container).data('imgSize');
		$(container).css({ position: 'fixed', marginBottom: '0', left: 0, right: 0, top: 0, bottom: 0, zIndex: 5, padding: '70px', background: 'rgba(0,0,0,0.5)' });
		if (typeof (d3) == "undefined") {
			$(container).css("filter", "progid:DXImageTransform.Microsoft.gradient(startColorstr=#70000000,endColorstr=#70000000)");
		}
		$(container).find('.dc_wrapper').first().css({ background: 'white' });
		$(container).find('.closeButton').first().css({ display: 'block' });
		$(container).find('figure').first().css({ margin: 'auto', width: imgSize.width + 'px', height: imgSize.height + 'px' });
		if ($(container).find('.dc_wrapper').height() > imgSize.height) $(container).find('figure').first().css({ top: '50%', marginTop: '-' + (imgSize.height / 2) + 'px' });
		$(container).find('.fullscreen').first().css({ display: 'none', position: 'absolute', right: '70px', width: 'calc(100% - 140px)', backgroundPosition: '100% -20px' });
		$(document.body).css('overflow', 'hidden');
	}
};
var hideDiagram = function () {
	if (currentDiagram != undefined) {
		var container = currentDiagram;
		var imgSize = $(container).data('imgSize');
		$(container).css({ position: 'relative', marginBottom: '30px', padding: '0', zIndex: 0, background: 'none' });
		if (typeof (d3) == "undefined") {
			$(container).css("filter", "none");
		}
		$(container).find('.dc_wrapper').first().css({ background: 'none' });
		$(container).find('.closeButton').first().css({ display: 'none' });
		$(container).find('figure').first().css({ margin: '0', width: imgSize.newWidth + 'px', height: imgSize.newHeight + 'px', top: '0' });
		$(container).find('.fullscreen').first().css({ display: 'block', position: 'relative', right: '0px', width: imgSize.newWidth + 'px', backgroundPosition: '100% 0px' });
		$(document.body).css('overflow', 'auto');
		currentDiagram = undefined;
	}
};

var smoothScroll = function (element) {
	if (!$(element).parent().hasClass('navitem')) { $('#txtSearch').val(''); checkSearch('', true); }
	var href = $.attr(element, 'href');
	return smoothScrollLocation(href);
};
var smoothScrollLocation = function (href) {
	if (typeof (href) == "undefined" || href.indexOf('#') != 0) return false;
	if ($(href)[0] == undefined) return false;
	hideDiagram(); $('#d3_treeview_container').hide();
	if(window.location.hash == href) window.location.hash = '';
	window.location.hash = href;
	return true;
};

$(document).keyup(function (e) {
	if (e.keyCode == 27) {
		hideDiagram(); $('#d3_treeview_container').hide();
	}
});

var d3Data = {};

var d3Margin = [0, 120, 20, 120];
var d3Width = 1280 - d3Margin[1] - d3Margin[3];
var d3Height = 800 - d3Margin[0] - d3Margin[2];

var d3Count = 0;
var d3Root;
var d3Vis;
var d3Zoom;

if (typeof (d3) !== "undefined") {
	var d3Tree = d3.layout.tree().size([d3Height, d3Width]);
	var d3Diagonal = d3.svg.diagonal().projection(function(d) { return [d.y, d.x]; });
}

function d3Update(source) {
	if (typeof (d3) == "undefined") return;

	var duration = d3.event && d3.event.altKey ? 5000 : 500;

	var nodes = d3Tree.nodes(d3Root).reverse();
	nodes.forEach(function (d) { d.y = d.depth * 180; });

	var node = d3Vis.selectAll('g.node').data(nodes, function (d) { return d.id || (d.id = ++d3Count); });

	var nodeEnter = node.enter().append('svg:g').attr('class', 'node').attr('transform', function (d) { return 'translate(' + source.y0 + ',' + source.x0 + ')' });
	nodeEnter.append('svg:circle').attr('r', 1e-6).style('fill', function (d) { return d._children ? 'lightsteelblue' : '#fff' }).on('click', function (d) { d3Toggle(d); d3Update(d) });

	var nodeLink = nodeEnter.append('svg:a').attr('xlink:href', function (d) { return d.url; }).attr('href', function (d) { return d.url; }).on('click', function (d) { $('#txtSearch').val(''); checkSearch('', true); smoothScrollLocation(d.url); });
	nodeLink.append('svg:text').attr('x', function (d) { return d.children || d._children ? -10 : 10 }).attr('y', '-10').attr('dy', '.35em').attr('text-anchor', function (d) { return d.children || d._children ? 'end' : 'start' }).text(function (d) { return d.name }).style('fill-opacity', 1e-6).style('text-shadow', 'white 1px 1px 5px, white 1px 1px 5px, white 1px 1px 5px, white 1px 1px 5px');

	var nodeUpdate = node.transition().duration(duration).attr('transform', function (d) { return 'translate(' + d.y + ',' + d.x + ')' });
	nodeUpdate.select('circle').attr('r', 4.5).style('fill', function (d) { return d._children ? 'lightsteelblue' : '#fff' });
	nodeUpdate.select('text').style('fill-opacity', 1);

	var nodeExit = node.exit().transition().duration(duration).attr('transform', function (d) { return 'translate(' + source.y + ',' + source.x + ')' }).remove();
	nodeExit.select('circle').attr('r', 1e-6); nodeExit.select('text').style('fill-opacity', 1e-6);

	var link = d3Vis.selectAll('path.link').data(d3Tree.links(nodes), function (d) { return d.target.id });
	link.enter().insert('svg:path', 'g').attr('class', 'link').attr('d', function (d) { var o = { x: source.x0, y: source.y0 }; return d3Diagonal({ source: o, target: o }) }).transition().duration(duration).attr('d', d3Diagonal);
	link.transition().duration(duration).attr('d', d3Diagonal);
	link.exit().transition().duration(duration).attr('d', function (d) { var o = { x: source.x, y: source.y }; return d3Diagonal({ source: o, target: o }) }).remove();

	nodes.forEach(function (d) { d.x0 = d.x; d.y0 = d.y });
}
function d3Toggle(d) {
	if (typeof (d3) == "undefined") return;

	if (d.children) {
		d._children = d.children;
		d.children = null;
	} else {
		d.children = d._children;
		d._children = null;
	}
}

var d3Move = function (translation) {
	if (typeof (d3) == "undefined") return;

	d3Zoom.translate(translation);
	d3Vis.attr('transform', 'translate(' + translation + ')');
};

var d3Redraw = function () {
	if (typeof (d3) == "undefined") return;

	d3Vis.attr('transform', 'translate(' + d3.event.translate + ')' + ' scale(1)');
};

$(document).ready(function () {
	$(document).on('propertychange keyup input paste', '#txtSearch', function () {
		var element = $(this);
		clearTimeout(window.searchDelay);
		window.searchDelay = setTimeout(function () {
			checkSearch(element.val());
			var io = element.val().length ? 1 : 0;
			element.next('.icon_clear').stop().fadeTo(300, io);
		}, 200);
	}).on('click', '.icon_clear', function () {
		$(this).fadeTo(300, 0).prev('input').val(''); checkSearch('');
	});

	if (typeof (d3) !== "undefined") {
		d3Vis = d3.select('#d3_treeview').append('svg:svg').attr('style', 'width:100%;height:100%').attr('viewBox', '0 0 ' + (d3Width + d3Margin[1] + d3Margin[3]) + ' ' + (800 - d3Margin[0] - d3Margin[2])).call((d3Zoom = d3.behavior.zoom()).scaleExtent([1, 1]).on('zoom', d3Redraw)).append('svg:g').attr('transform', 'translate(' + d3Margin[3] + ',' + d3Margin[0] + ')');
	} else {
		$('#txtSearch').css('background', 'none');
		$('.btn_d3Visualize').css('display', 'none');
		$('.diagramFigure').css('display', 'none');
		$('.diagramImage').css('display', 'block');
	}
	$('#pleaseWait').css('display', 'none'); $('#mainContent').css('display', 'block');

	$('a').click(function () { smoothScroll(this); });

	$('#txtSearch').keyup(function (e) { if (e.keyCode == 27) { $(this).val(''); checkSearch(''); } });
	$('.clearable').each(function (key, value) {
		$(value).find('.icon_clear').first().qtip(
		{
			content: 'Clear search string. (Esc)',
			show: { delay: 500 },
			position:
			{
				target: 'mouse',
				adjust: { mouse: false },
				viewport: $(window)
			}
		});
	});

	$('.closeButton').each(function (key, value) {
		$(value).qtip(
		{
			content: 'Close image. (Esc)',
			show: { delay: 500 },
			position:
			{
				target: 'mouse',
				adjust: { mouse: false },
				viewport: $(window)
			}
		});
	});

	$('.fullscreen').each(function (key, value) {
		$(value).qtip(
		{
			content: 'Expand image to its original size.',
			show: { delay: 500 },
			position:
			{
				target: 'mouse',
				adjust: { mouse: false },
				viewport: $(window)
			}
		});
	});

	$('#d3_treeview_container, #d3_treeview_container .closeButton, #d3_treeview_container .closeButton div').mousedown(function (e) {
		if (e.target == this) $('#d3_treeview_container').hide();
	});
	$('.diagramContainer, .diagramContainer .closeButton, .diagramContainer .closeButton div').click(function (e) {
		if (e.target == this) hideDiagram();
	});
	$('.diagramContainer .fullscreen').click(function () {
		if (currentDiagram == undefined) showDiagram($(this).parent()[0]);
		else hideDiagram();
	});

	$('#nav').find('.navitem').each(function (key, value) {
		$(value).find('a').qtip(
		{
			content: $('#tooltip_' + $(value).find('a').attr('href').replace('#', '')).html(),
			show: { delay: 600 },
			position:
			{
				target: 'mouse',
				adjust: { mouse: true },
				viewport: $(window)
			}
		});
	});

	var plainHash = document.location.hash;
	var hash = document.location.hash;
	if (hash.indexOf('{') < 0 && $('.section' + hash)[0] == undefined) {
		document.location = document.location.href.replace(document.location.hash, '') + '#' + plainHash.replace('#', '');
	}
	if (hash != undefined) {
		$('#txtSearch').val(''); checkSearch('', true); smoothScrollLocation(location.hash);
	}

	$('.diagramContainer rect').each(function (key, value) {
		if ($('#tooltip_' + $(value).attr('id')).length > 0) {
			$(value).qtip({
				content: $('#tooltip_' + $(value).attr('id')).html(),
				show: { delay: 500 },
				events: {
					render: function (e) {
						$(e.target).find('a').click(function () {
							smoothScroll(this);
						});
					}
				},
				position: {
					target: 'mouse',
					adjust: { mouse: false },
					viewport: $(window)
				}
			});
		}
	});

	$('.tablesorter').tablesorter({ sortList: [[1, 0]] });
	$('ul > br').remove();
});