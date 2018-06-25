Haack.ready(function() {
	var images = document.querySelectorAll('.post-content img')
	images.forEach(function(img) {
		var alt = img.alt
		var parent = img.parentElement

		if (alt && alt != '') {
			var element = img
			if (parent.tagName == 'A') {
				element = parent;
			}
			element.insertAdjacentHTML('afterend', '<span class="caption">' + alt + '</span>');

/*			if (!parent.is('a')) {
				$(this).wrap('<a href="'+this.src+'" title="'+alt+'" />');
			}*/
		}
	})
})
