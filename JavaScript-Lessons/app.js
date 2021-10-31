const person = {
    name: 'Andrey',
    age: 19,
    isProgrammer: true,
    languages: ['ru', 'en', 'ua'],
    greet(){
        console.log('greet from person')
    }
}


console.log(person.name)
console.log(person['age'])