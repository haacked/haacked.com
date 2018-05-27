Haack.ready(function() {

  let form = Haack.get('commentform')

  if (form) {
    var emailRegex = /[^@\s]+@[^@\s]+\.[^@\s]+$/
    var avatarPreview = Haack.get('avatarPreview')
    avatarPreview.onerror = (e) => { tryLoad(e.target, 1) }

    function changeAvatar() {
      let image = avatarPreview
      image.possible = buildPossibleAvatars(Haack.get('identity').value)
      image.currentIndex = 0
      tryLoad(image)
    }

    function tryLoad(image, increment) {
      if (increment) {
        image.currentIndex += increment
      }

      if (image.currentIndex < image.possible.length) {
        image.src = image.possible[image.currentIndex]
      }
      else {
        image.onerror = null
        image.src = image.dataset.fallbacksrc;
      }
    }

    function buildPossibleAvatars(identity) {
      let possibleAvatars = []

      if (identity.match(emailRegex)) {
        possibleAvatars.push('https://secure.gravatar.com/avatar/' + md5(identity) + '?s=80&d=identicon&r=pg')
      } else {
        possibleAvatars.push('https://github.com/' + identity + '.png')
        possibleAvatars.push('https://avatars.io/twitter/' + identity + '/medium')
      }

      return possibleAvatars
    }

    Haack.get('comment-div').oninput = (e) => {
      Haack.get('message').value = e.target.innerText
    }

    Haack.get('identity').onchange = () => {
      changeAvatar()
    }

    function storeUser(name, identity) {
      window.localStorage.name = name;
      window.localStorage.identity = identity;
    }

    function retrieveUser(nameInput, identityInput, rememberCheckbox) {
      var rememberMe = false
      if (window.localStorage.name) {
        nameInput.value = window.localStorage.name;
        rememberMe = true
      }
      if (window.localStorage.identity) {
        identityInput.value = window.localStorage.identity;
        rememberMe = true
      }
      if (rememberMe) {
        rememberCheckbox.checked = true
      }
    }

    Haack.get('commentbutton').onclick = (e) => {
      let status = Haack.get('commentstatus')
      status.innerText = ''

      let missing = Array.from(form.querySelectorAll('[data-required]')).filter(el => el.value === '').map(el => el.name)
      if (missing.length > 0) {
        status.innerText = 'Some required fields are missing - (' + missing.join(', ') + ')'
        return
      }
      let button = e.target
      if (button.innerText != 'Confirm comment') {
        button.innerText = 'Confirm comment'
        button.title = 'Click the button again to confirm the comment'
        button.classList.add('confirm-button')
        return
      }
      let name = Haack.get('name')
      let identity = Haack.get('identity')

      if (Haack.get('remember').checked) {
        storeUser(name.value, identity.value)
      }
      else {
        storeUser('', '')
      }
      Haack.get('avatarInput').value = avatarPreview.src

      button.disabled = true
      button.innerText = 'Posting...'
      identity.value = ""
      form.action = form.dataset.action
      form.submit()
    }

    // Load values from Local Storage.
    retrieveUser(Haack.get('name'), Haack.get('identity'), Haack.get('remember'))
    changeAvatar() // initial load of avatar
  }
})
