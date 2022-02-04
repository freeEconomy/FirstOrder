/*
	Escape Velocity by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
*/

(function($) {

	var	$window = $(window),
		$body = $('body');

	// Breakpoints.
		breakpoints({
			xlarge:  [ '1281px',  '1680px' ],
			large:   [ '981px',   '1280px' ],
			medium:  [ '737px',   '980px'  ],
			small:   [ null,      '736px'  ]
		});

	// Play initial animations on page load.
		$window.on('load', function() {
			window.setTimeout(function() {
				$body.removeClass('is-preload');
			}, 100);
		});

	// Dropdowns.
		$('#nav > ul').dropotron({
			mode: 'fade',
			noOpenerFade: true,
			alignment: 'center',
			detach: false
		});

	// Nav.

		// Title Bar.
			$(
				'<div id="titleBar">' +
					'<a href="#navPanel" class="toggle"></a>' +
					'<span class="title">' + $('#logo h1').html() + '</span>' +
				'</div>'
			)
				.appendTo($body);

		// Panel.
			$(
				'<div id="navPanel">' +
					'<nav>' +
						$('#nav').navList() +
					'</nav>' +
				'</div>'
			)
				.appendTo($body)
				.panel({
					delay: 500,
					hideOnClick: true,
					hideOnSwipe: true,
					resetScroll: true,
					resetForms: true,
					side: 'left',
					target: $body,
					visibleClass: 'navPanel-visible'
				});


    // Menu.
			var $menu = $('#menu'),
                $menuInner;

			$menu.wrapInner('<div class="inner"></div>');
			$menuInner = $menu.children('.inner');
			$menu._locked = false;

			$menu._lock = function () {

			    if ($menu._locked)
			        return false;

			    $menu._locked = true;

			    window.setTimeout(function () {
			        $menu._locked = false;
			    }, 350);

			    return true;

			};

			$menu._show = function () {

			    if ($menu._lock())
			        $body.addClass('is-menu-visible');

			};

			$menu._hide = function () {

			    if ($menu._lock())
			        $body.removeClass('is-menu-visible');

			};

			$menu._toggle = function () {

			    if ($menu._lock())
			        $body.toggleClass('is-menu-visible');

			};

			$menuInner
                .on('click', function (event) {
                    event.stopPropagation();
                })
                .on('click', 'a', function (event) {

                    var href = $(this).attr('href');

                    event.preventDefault();
                    event.stopPropagation();

                    // Hide.
                    $menu._hide();

                    // Redirect.
                    window.setTimeout(function () {
                        window.location.href = href;
                    }, 250);

                });

			$menu
                .appendTo($body)
                .on('click', function (event) {

                    event.stopPropagation();
                    event.preventDefault();

                    $body.removeClass('is-menu-visible');

                })
                .append('<a class="close" href="#menu">Close</a>');

			$body
                .on('click', 'a[href="#menu"]', function (event) {

                    event.stopPropagation();
                    event.preventDefault();

                    // Toggle.
                    $menu._toggle();

                })
                .on('click', function (event) {

                    // Hide.
                    $menu._hide();

                })
                .on('keydown', function (event) {

                    // Hide on escape.
                    if (event.keyCode == 27)
                        $menu._hide();

                });

})(jQuery);