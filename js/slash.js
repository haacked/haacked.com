(function($) {
	// Append caption after pictures
	$('.entry-content').each(function(i) {
		var _i = i;
		$(this).find('img').each(function() {
			var alt = this.alt;

			var parent = $(this).parent();

			if (alt != '') {
				var element = $(this);
				if (parent.is('a')) {
					element = parent;
				}
				element.after('<span class="caption">'+alt+'</span>');
			}

			if (!parent.is('a')) {
				$(this).wrap('<a href="'+this.src+'" title="'+alt+'" class="fancybox" rel="gallery'+_i+'" />');
			}
			else {
				parent.addClass('fancybox');
				parent.attr('rel', 'gallery' + _i)
			}
		});
	});
})(jQuery);