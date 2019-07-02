Feature: Game

    Scenario: Empty League
	    Given the league has no players
	    When I print the league
	    Then I should see "No players yet"

	Scenario: One player in League
		Given the league has one player added
		When I print the league
		Then I should have one player
